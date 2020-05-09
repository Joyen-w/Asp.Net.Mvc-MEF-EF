using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public interface ISiteConfig
    {
        string Domain { get; set; }
        int Port { get; set; }
    }
}
