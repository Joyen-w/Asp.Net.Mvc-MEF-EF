using DAL.Annotations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class ExtendFieldAttributeConvention : Convention
    {
        public ExtendFieldAttributeConvention()
        {
            base.Properties<string>().Having<object>((System.Reflection.PropertyInfo p) => p.GetCustomAttributes(typeof(ExtendContentFieldAttribute), true).FirstOrDefault<object>()).Configure(delegate (ConventionPrimitivePropertyConfiguration c, object a)
            {
                c.HasColumnType("xml");
            }
            );
        }
    }
}
