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
    public class IdAttributeConvention : Convention
    {
        public IdAttributeConvention()
        {
            this.Properties<int>().Having<object>((System.Reflection.PropertyInfo p) => p.GetCustomAttributes(typeof(IdAttribute), true).FirstOrDefault<object>()).Configure(delegate (ConventionPrimitivePropertyConfiguration c, object a)
            {
                c.IsKey();
            }
            );
        }
    }
}
