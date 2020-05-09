using DAL.Annotations;
using DAL.Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Web
{
    internal class ModelTypeDataProvider
    {
        internal static ModelTypeData ReflectClass(System.Type type)
        {
            ModelTypeData modelTypeData = new ModelTypeData();
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
            DisplayColumnAttribute displayColumnAttribute = type.GetCustomAttribute(typeof(DisplayColumnAttribute), true) as DisplayColumnAttribute;
            if (displayColumnAttribute != null)
            {
                string displayColumnPropertyName = displayColumnAttribute.DisplayColumn;
                modelTypeData.DisplayColumnProperty = properties.FirstOrDefault((System.Reflection.PropertyInfo x) => x.Name == displayColumnPropertyName);
            }
            System.Reflection.PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                System.Reflection.PropertyInfo propertyInfo = array[i];
                object obj = propertyInfo.GetCustomAttributes(typeof(IdAttribute), true).FirstOrDefault<object>();
                if (obj != null)
                {
                    modelTypeData.IdProperty = propertyInfo;
                    break;
                }
            }
            System.Reflection.PropertyInfo propertyInfo2 = properties.FirstOrDefault((System.Reflection.PropertyInfo x) => x.IsDefined(typeof(SuperordinateIdAttribute), true));
            if (propertyInfo2 != null)
            {
                modelTypeData.SuperordinateIdProperty = propertyInfo2;
            }
            return modelTypeData;
        }
    }
}
