using DAL.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Utilities
{
    public static class ModelHelper
    {
        public static System.Reflection.PropertyInfo GetPrimaryKeyInfo(System.Type modelType)
        {
            System.Reflection.PropertyInfo[] properties = modelType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            System.Reflection.PropertyInfo result;
            for (int i = 0; i < properties.Length; i++)
            {
                System.Reflection.PropertyInfo propertyInfo = properties[i];
                if (propertyInfo.IsPrimaryKey())
                {
                    result = propertyInfo;
                    return result;
                }
            }
            result = null;
            return result;
        }
        public static bool IsPrimaryKey(this System.Reflection.PropertyInfo propertyInfo)
        {
            return System.Attribute.IsDefined(propertyInfo, typeof(IdAttribute));
        }
    }
}
