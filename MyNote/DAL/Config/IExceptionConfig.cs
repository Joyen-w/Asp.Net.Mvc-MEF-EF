using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public interface IExceptionConfig
    {
        int ErrorMessageType { get; set; }
        bool LogEnable { get; set; }
        int RecordTimeSpan { get; set; }
        bool NotFoundEnabled { get; set; }
        string NotFoundView { get; set; }
    }
}
