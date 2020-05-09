using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public static class SecurityConfig
    {
        public static ISecurityConfig Instance
        {
            get
            {
                return GlobalConfigProvider.Current.GetSecurityConfig();
            }
        }
    }
}
