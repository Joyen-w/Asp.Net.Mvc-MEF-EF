using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public interface IMailConfig
    {
        bool EnabledSsl { get; set; }
        string MailFrom { get; set; }
        int Port { get; set; }
        AuthenticationType AuthenticationType { get; set; }
        string MailServer { get; set; }
        string MailServerUserName { get; set; }
        string MailServerPassWord { get; set; }
    }
}
