using DAL.Dal;
using DAL.DBContext;
using DAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Search
{
    public class SearchEngine<TModel, TRepository> : ISearchEngine<TModel> where TModel : class where TRepository : IRepository<TModel>, new()
    {
        public TRepository Repository { get; set; }
        public SearchEngine()
        {
            TRepository tRepository = default(TRepository);
            TRepository tRepository2;
            if (tRepository != null)
            {
                tRepository = default(TRepository);
                tRepository2 = tRepository;
            }
            else
            {
                tRepository2 = Activator.CreateInstance<TRepository>();
            }
            this.Repository = tRepository2;
        }
        public IEnumerable<TModel> GetMany(ISearchParameters searchParameters, int outputCount = 0)
        {
            Check.NotNull(searchParameters, "searchParameters");
            Seeker1<TModel> seeker = new Seeker1<TModel>(searchParameters, null);
            object[] whereParameters;
            string simpleFilterConstraint = seeker.GetSimpleFilterConstraint(out whereParameters);
            string orderConstraint = seeker.GetOrderConstraint();
            IEnumerable<TModel> many = null;
            //if (outputCount == 0)
            //{
            //    TRepository repository = this.Repository;
            //    many = repository.GetMany(simpleFilterConstraint, whereParameters, orderConstraint);
            //}
            //else
            //{
            //    TRepository repository = this.Repository;
            //    many = repository.GetMany(simpleFilterConstraint, whereParameters, orderConstraint, outputCount);
            //}
            return many;
        }
        //public IPagedList<TModel> GetPagedList(PageSearchParameters httpSearchParameters)
        //{
        //    Check.NotNull(httpSearchParameters, "gridParameters");
        //    Seeker1<TModel> seeker = new Seeker1<TModel>(httpSearchParameters, null);
        //    object[] whereParameters;
        //    string simpleFilterConstraint = seeker.GetSimpleFilterConstraint(out whereParameters);
        //    string orderConstraint = seeker.GetOrderConstraint();
        //    TRepository repository = this.Repository;
        //    return repository.GetPagedList(httpSearchParameters.PageIndex, httpSearchParameters.PageSize, simpleFilterConstraint, whereParameters, orderConstraint);
        //}
    }
}
