using DAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Search
{
    public static class SearchSortConstraintBinder<TModel> where TModel : class
    {
        public static string GetSortConstraint(string sortColumn, string sortOrder)
        {
            string arg = (string.IsNullOrEmpty(sortOrder) || sortOrder.ToLower() == "asc") ? "asc" : "desc";
            return string.Format("it.{0} {1}", string.IsNullOrEmpty(sortColumn) ? GetPrimaryKeyName(typeof(TModel)) : sortColumn, arg);
        }
        private static string GetPrimaryKeyName(Type type)
        {
            System.Reflection.PropertyInfo[] arrProperty = type.GetProperties();
            string result;
            for (int i = 0; i < arrProperty.Length; i++)
            {
                System.Reflection.PropertyInfo propertyInfo = arrProperty[i];
                if (propertyInfo.IsPrimaryKey())
                {
                    result = propertyInfo.Name;
                    return result;
                }
            }
            arrProperty = type.GetProperties();
            for (int i = 0; i < arrProperty.Length; i++)
            {
                System.Reflection.PropertyInfo propertyInfo = arrProperty[i];
                if (propertyInfo.Name.ToLower() == "id")
                {
                    result = propertyInfo.Name;
                    return result;
                }
            }
            result = type.Name + "Id";
            return result;
        }
    }
}
