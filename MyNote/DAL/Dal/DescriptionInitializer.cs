using DAL.Annotations;
using DAL.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dal
{
    public class DescriptionInitializer<T> : IDatabaseInitializer<T> where T : BaseDbContext<T>, new()
    {
        private const string SelectQueryTemplate = "SELECT 1 FROM ::fn_listextendedproperty (default,'schema',N'dbo', 'table', {1}, {2}, {3}) where name='MS_DESCRIPTION'";
        private const string AddDescriptionQueryTemplate = "EXEC sp_addextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3}";
        private const string UpdateDescriptionQueryTemplate = "EXEC sp_updateextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3}";
        private const string AddOrUpdateQueryTemplate = "IF EXISTS(SELECT 1 FROM ::fn_listextendedproperty (default,'schema',N'dbo', 'table', {1}, {2}, {3}) where name='MS_DESCRIPTION') BEGIN EXEC sp_updateextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3} END ELSE BEGIN EXEC sp_addextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3} END";
        private const string SqlTableDescription = "IF EXISTS(SELECT 1 FROM syscolumns WHERE id = OBJECT_ID({1})) BEGIN IF EXISTS(SELECT 1 FROM ::fn_listextendedproperty (default,'schema',N'dbo', 'table', {1}, {2}, {3}) where name='MS_DESCRIPTION') BEGIN EXEC sp_updateextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3} END ELSE BEGIN EXEC sp_addextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3} END END";
        private const string SqlColumnDescription = "IF EXISTS(SELECT 1 FROM syscolumns WHERE id = OBJECT_ID({1}) AND NAME = {3}) BEGIN IF EXISTS(SELECT 1 FROM ::fn_listextendedproperty (default,'schema',N'dbo', 'table', {1}, {2}, {3}) where name='MS_DESCRIPTION') BEGIN EXEC sp_updateextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3} END ELSE BEGIN EXEC sp_addextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3} END END";
        public void InitializeDatabase(T context)
        {
            foreach (System.Reflection.PropertyInfo current in
                from p in typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                where p.PropertyType.Name == typeof(DbSet).Name
                select p)
            {
                System.Type type = current.PropertyType.GetGenericArguments().Single<System.Type>();
                string realTableName = DescriptionInitializer<T>.GetRealTableName(context, type);
                DescriptionInitializer<T>.AddTableDescription(context, type, realTableName);
                System.Reflection.PropertyInfo[] properties = type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                for (int i = 0; i < properties.Length; i++)
                {
                    System.Reflection.PropertyInfo propertyInfo = properties[i];
                    if (!propertyInfo.IsDefined(typeof(NotMappedAttribute), false))
                    {
                        DescriptionInitializer<T>.AddColumnDescription(context, propertyInfo, realTableName);
                    }
                }
            }
        }
        private static void AddTableDescription(T context, System.Type entityType, string tableName)
        {
            string memberDescription = DescriptionInitializer<T>.GetMemberDescription(entityType);
            if (!string.IsNullOrEmpty(memberDescription))
            {
                Database arg_35_0 = context.Database;
                string arg_35_1 = "IF EXISTS(SELECT 1 FROM syscolumns WHERE id = OBJECT_ID({1})) BEGIN IF EXISTS(SELECT 1 FROM ::fn_listextendedproperty (default,'schema',N'dbo', 'table', {1}, {2}, {3}) where name='MS_DESCRIPTION') BEGIN EXEC sp_updateextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3} END ELSE BEGIN EXEC sp_addextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3} END END";
                object[] array = new object[4];
                array[0] = memberDescription;
                array[1] = tableName;
                arg_35_0.ExecuteSqlCommand(arg_35_1, array);
            }
        }
        private static void AddColumnDescription(T context, System.Reflection.PropertyInfo property, string tableName)
        {
            string text = DescriptionInitializer<T>.GetMemberDescription(property) ?? DescriptionInitializer<T>.GetDisplayName(property);
            if (!string.IsNullOrEmpty(text))
            {
                context.Database.ExecuteSqlCommand("IF EXISTS(SELECT 1 FROM syscolumns WHERE id = OBJECT_ID({1}) AND NAME = {3}) BEGIN IF EXISTS(SELECT 1 FROM ::fn_listextendedproperty (default,'schema',N'dbo', 'table', {1}, {2}, {3}) where name='MS_DESCRIPTION') BEGIN EXEC sp_updateextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3} END ELSE BEGIN EXEC sp_addextendedproperty N'MS_Description', {0}, 'SCHEMA', N'dbo', 'TABLE', {1}, {2}, {3} END END", new object[]
                {
                    text,
                    tableName,
                    "COLUMN",
                    DescriptionInitializer<T>.GetRealColumnName(property)
                });
            }
        }
        private static string GetRealTableName(T context, System.Type entityType)
        {
            TableAttribute[] array = (TableAttribute[])entityType.GetCustomAttributes(typeof(TableAttribute), false);
            return (array.Length != 0) ? array[0].Name : context.GetTableName(entityType);
        }
        private static string GetRealColumnName(System.Reflection.PropertyInfo property)
        {
            ColumnAttribute columnAttribute = property.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault<object>() as ColumnAttribute;
            return (columnAttribute != null) ? columnAttribute.Name : property.Name;
        }
        private static string GetMemberDescription(System.Reflection.MemberInfo memberInfo)
        {
            string result = null;
            DescriptionAttribute descriptionAttribute = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault<object>() as DescriptionAttribute;
            if (descriptionAttribute != null)
            {
                result = descriptionAttribute.Description;
            }
            return result;
        }
        private static string GetDisplayName(System.Reflection.PropertyInfo property)
        {
            string result = null;
            BaseAttribute baseAttribute = property.GetCustomAttributes(typeof(BaseAttribute), true).FirstOrDefault<object>() as BaseAttribute;
            if (baseAttribute != null)
            {
                result = baseAttribute.DisplayName;
            }
            return result;
        }
    }
}
