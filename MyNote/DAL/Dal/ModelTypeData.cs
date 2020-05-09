using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dal
{
    public class ModelTypeData
    {
        public System.Reflection.PropertyInfo DisplayColumnProperty { get; set; }
        public System.Reflection.PropertyInfo IdProperty { get; set; }
        public System.Reflection.PropertyInfo SuperordinateIdProperty { get; set; }
        public string GetDisplayColumnName()
        {
            return (this.DisplayColumnProperty == null) ? string.Empty : this.DisplayColumnProperty.Name;
        }
        public string GetDisplayColumnValue(object obj)
        {
            return (this.DisplayColumnProperty == null) ? string.Empty : (this.DisplayColumnProperty.GetValue(obj) as string);
        }
        public string GetIdName()
        {
            return (this.IdProperty == null) ? string.Empty : this.IdProperty.Name;
        }
        public int GetIdValue(object obj)
        {
            return (this.IdProperty == null) ? 0 : System.Convert.ToInt32(this.IdProperty.GetValue(obj));
        }
        public string GetSuperordinateIdName()
        {
            return (this.SuperordinateIdProperty == null) ? string.Empty : this.SuperordinateIdProperty.Name;
        }
        public int GetSuperordinateIdValue(object obj)
        {
            return (this.SuperordinateIdProperty == null) ? 0 : System.Convert.ToInt32(this.SuperordinateIdProperty.GetValue(obj));
        }
        public void SetSuperordinateIdValue(object obj, int foreignKeyValue)
        {
            if (this.SuperordinateIdProperty != null)
            {
                this.SuperordinateIdProperty.SetValue(obj, foreignKeyValue);
            }
        }
    }
}
