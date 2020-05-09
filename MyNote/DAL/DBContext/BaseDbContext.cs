using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DAL.Properties;
using MefBase.BaseEF;
using MefBase.IBaseEF;
using System.Data.Entity;
using DAL.Utilities;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Configuration;
using DAL.Dal;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using DAL.Annotations;

namespace DAL.DBContext
{
    public class BaseDbContext<TContext> : DbContext where TContext : BaseDbContext<TContext>, new()
    {
        protected BaseDbContext() : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Check.NotNull<DbModelBuilder>(modelBuilder, "modelBuilder");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Types().Configure(delegate (ConventionTypeConfiguration entity)
            {
                entity.ToTable(this.GetTableName(entity.ClrType));
            }
            );
            modelBuilder.Conventions.Add<StringAttributeMaxLengthConvention>();
            modelBuilder.Conventions.Add<IdAttributeConvention>();
            modelBuilder.Conventions.Add<ExtendFieldAttributeConvention>();
            InitializerComposite<TContext> initializerComposite = new InitializerComposite<TContext>(new IDatabaseInitializer<TContext>[0]);
            initializerComposite.AddInitializer(new DescriptionInitializer<TContext>());
            Database.SetInitializer<TContext>(initializerComposite);
        }
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, System.Collections.Generic.IDictionary<object, object> items)
        {
            Check.NotNull<DbEntityEntry>(entityEntry, "entityEntry");
            DbEntityValidationResult dbEntityValidationResult = base.ValidateEntity(entityEntry, items);
            DbEntityValidationResult result;
            if (dbEntityValidationResult.IsValid)
            {
                result = dbEntityValidationResult;
            }
            else
            {
                System.Collections.Generic.List<string> unverifiedPropertyList = new System.Collections.Generic.List<string>();
                System.Reflection.PropertyInfo[] properties = entityEntry.Entity.GetType().GetProperties();
                System.Reflection.PropertyInfo[] array = properties;
                for (int i = 0; i < array.Length; i++)
                {
                    System.Reflection.PropertyInfo propertyInfo = array[i];
                    if (propertyInfo.IsDefined(typeof(NotMappedAttribute), false))
                    {
                        unverifiedPropertyList.Add(propertyInfo.Name);
                    }
                    if (entityEntry.State == EntityState.Unchanged && propertyInfo.IsDefined(typeof(UnmodifiableAttribute), false))
                    {
                        if (!unverifiedPropertyList.Contains(propertyInfo.Name))
                        {
                            unverifiedPropertyList.Add(propertyInfo.Name);
                        }
                    }
                }
                result = new DbEntityValidationResult(entityEntry,
                    from r in dbEntityValidationResult.ValidationErrors
                    where !unverifiedPropertyList.Contains(r.PropertyName)
                    select r);
            }
            return result;
        }
    }
    public static class DbContextExtension
    {
        public static string GetTableName(this DbContext dbContext, System.Type type)
        {
            Check.NotNull<System.Type>(type, "type");
            string fullName = type.Assembly.FullName;
            string text = fullName.Substring(0, fullName.IndexOf(",", System.StringComparison.Ordinal));
            return "PE_{module}_{tablename}".Replace("{module}", text.Substring(text.LastIndexOf('.') + 1)).Replace("{tablename}", type.Name);
        }
    }
}
