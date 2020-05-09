using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public interface ISecurityConfig
    {
        string ManagePath { get; set; }
        string AdministratorPasswordHashCode{ get; set; }
        int TicketTime { get; set; }
        bool EnableSiteManageCode { get; set; }
        string SiteManageCode { get; set; }
        bool EnableValidateUrlReferrer { get; set; }
        bool EnableSoftKeyBoardInput
        { get; set; }
        bool EnableConnectionStringProtect { get; set; }
        bool EnableLowerUrl { get; set; }
        string ServiceInstraction { get; set; }
    }
}
