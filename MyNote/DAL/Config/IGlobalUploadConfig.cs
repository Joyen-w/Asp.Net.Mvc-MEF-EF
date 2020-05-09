using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public interface IGlobalUploadConfig : IUploadConfig
    {
        string UploadDirectory { get; set; }
        string UploadPathPerfix { get; set; }
    }
}
