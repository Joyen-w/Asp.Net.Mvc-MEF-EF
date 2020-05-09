using DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Search
{
    public interface ISearchEngine<out TModel> where TModel : class
    {
        //IPagedList<TModel> GetPagedList(PageSearchParameters httpSearchParameters);
        System.Collections.Generic.IEnumerable<TModel> GetMany(ISearchParameters searchParameters, int outputCount = 0);
    }
}
