using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public interface IUserConfig
    {
        bool EnableRegister { get; set; }
        string Protocol { get; set; }
        int UserNameLimit { get; set; }
        int UserNameMax { get; set; }
        string UserNameRegisterDisabled { get; set; }
        bool EmailCheckRegister { get; set; }
        string EmailOfRegisterCheck { get; set; }
        string UserGroup { get; set; }
        bool SendMail { get; set; }
        string MailContent { get; set; }
        bool EnableCheckCodeOfLogin { get; set; }
    }
}
