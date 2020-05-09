using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Utilities
{
    public static class DataSourceHelper
    {
        private const string Suffix = "_DataSource";
        public static string GetDataSourceViewDataKey<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            string name = null;
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)expression.Body;
                name = ((memberExpression.Member is System.Reflection.PropertyInfo) ? memberExpression.Member.Name : null);
            }
            return DataSourceHelper.GetDataSourceViewDataKey(name);
        }
        public static string GetDataSourceViewDataKey(string name)
        {
            return name + "_DataSource";
        }
        public static void IntoModelMetadata(ModelMetadata metadata, string dataSourceName, string key = "DataSource")
        {
            if (string.IsNullOrWhiteSpace(dataSourceName))
            {
                dataSourceName = metadata.PropertyName;
            }
            string dataSourceViewDataKey = DataSourceHelper.GetDataSourceViewDataKey(dataSourceName);
            metadata.AdditionalValues.Add(key, dataSourceViewDataKey);
        }
    }
}
