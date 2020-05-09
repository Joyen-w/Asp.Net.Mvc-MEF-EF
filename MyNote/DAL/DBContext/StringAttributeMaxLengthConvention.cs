using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Properties;
using DAL.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace DAL.DBContext
{
    public class StringAttributeMaxLengthConvention : Convention
    {
        public StringAttributeMaxLengthConvention()
        {
            this.Properties<string>().Having<StringAttribute>((System.Reflection.PropertyInfo p) => (StringAttribute)p.GetCustomAttributes(typeof(StringAttribute), true).FirstOrDefault<object>()).Configure(delegate (ConventionPrimitivePropertyConfiguration c, StringAttribute a)
            {
                c.HasMaxLength(a.ColumnMaxLength);
            }
            );
        }
    }
}
