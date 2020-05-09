using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Utilities
{
    public static class TypeExtensions
    {
        public static string GetDisplayName(this System.Type type, string fieldName)
        {
            DisplayAttribute displayAttribute = type.GetField(fieldName).GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault<object>() as DisplayAttribute;
            return (displayAttribute != null) ? displayAttribute.Name : fieldName;
        }
    }
}
