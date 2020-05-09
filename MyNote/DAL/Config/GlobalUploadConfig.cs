using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public class GlobalUploadConfig
    {
        public static IGlobalUploadConfig Instance
        {
            get
            {
                return GlobalConfigProvider.Current.GetGlobalUploadConfig();
            }
        }
    }
}
