using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUploadConfig
    {
        bool EnableUpload { get; set; }
        bool EnableAutoUpload { get; set; }
        long FileSingleSizeLimit { get; set; }
        string Extensions { get; set; }
        string UploadPathRule { get; set; }
        string UploadRuleKey { get; set; }
        string UploadUrl { get; set; }
        string UploadProviderKey { get; set; }
        bool EnableWatermark { get; set; }
    }
}
