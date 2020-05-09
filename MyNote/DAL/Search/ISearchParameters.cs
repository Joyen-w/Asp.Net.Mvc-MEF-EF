using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Search
{
    public interface ISearchParameters
    {
        string SortColumn { get; set; }
        string SortOrder { get; set; }
        SearchConstraint Where { get; set; }
    }
}
