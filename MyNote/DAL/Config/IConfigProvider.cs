using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public interface IConfigProvider
    {
        ISecurityConfig GetSecurityConfig();
        ISiteConfig GetSiteConfig();
        IExceptionConfig GetExceptionConfig();
        IMailConfig GetMailConfig();
        IUserConfig GetUserConfig();
        IGlobalUploadConfig GetGlobalUploadConfig();
        IWatermarkConfig GetWatermarkConfig();
    }
}
