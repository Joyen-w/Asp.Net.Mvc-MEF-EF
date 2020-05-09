using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public static class GlobalConfigProvider
    {
        public static IConfigProvider Current
        {
            internal get;
            set;
        }
    }
}
