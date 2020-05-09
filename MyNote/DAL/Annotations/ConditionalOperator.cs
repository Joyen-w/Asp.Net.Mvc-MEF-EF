using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Annotations
{
    [System.Flags]
    public enum ConditionalOperator
    {
        Equal = 1,
        NotEqual = 2,
        LessThan = 4,
        LessThanOrEqual = 8,
        GreaterThan = 16,
        GreaterThanOrEqual = 32,
        BeginsWith = 64,
        NotBeginsWith = 128,
        Contains = 256,
        NotContains = 512,
        EndsWith = 1024,
        NotEndsWith = 2048
    }
}
