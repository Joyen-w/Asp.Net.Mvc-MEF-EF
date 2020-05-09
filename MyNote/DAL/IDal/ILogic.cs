using DAL.DBContext;
using DAL.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dal
{
    public interface ILogic<TModel> where TModel : class
    {
        int Count();
        int Count(Expression<Func<TModel, bool>> where);
        IQueryable<TModel> GetAll();
        IQueryable<TModel> GetAll(bool createProxy);
        bool Exists(Expression<Func<TModel, bool>> where);
        bool RemoteExists(TModel entity);
        TModel GetEntity(int id);
        TModel GetEntity(Expression<Func<TModel, bool>> where);
        IEnumerable<TModel> GetMany(Expression<Func<TModel, bool>> where);
        IEnumerable<TModel> GetMany(ISearchParameters searchParameters);
        void Add(TModel entity);
        void Delete(int id);
        void BatchDelete(ICollection<int> ids);
        void BatchDelete(Expression<Func<TModel, bool>> where);
        void BatchDelete(ICollection<int> ids, Func<TModel, int> keyExpression);
        void DeleteAll();
        void Update(TModel entity);
        void Modify(TModel entity);
        void BatchUpdate(Expression<Func<TModel, bool>> where, Expression<Func<TModel, TModel>> update);
        void BatchUpdataSort(ICollection<TModel> models);
        void Save();
        //IPagedList<TModel> GetPagedList(PageSearchParameters gridParameters);
        //IPagedList<TModel> GetPagedList(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression);
        //IPagedList<TModel> GetPagedListByComplexFilter(PageSearchParameters gridParameters);
        //IPagedList<TModel> GetPagedListByComplexFilter(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression);
        //IPagedList<TModel> GetPagedListByComplexFilter(PageSearchParameters gridParameters, params Expression<Func<TModel, object>>[] includes);
        //IPagedList<TModel> GetPagedListByComplexFilter(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression, params Expression<Func<TModel, object>>[] includes);
        //IEnumerable<TModel> GetList(PageSearchParameters gridParameters);
        //IEnumerable<TModel> GetList(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression);
        //IEnumerable<TModel> GetListByComplexFilter(PageSearchParameters gridParameters);
        //IEnumerable<TModel> GetListByComplexFilter(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression);
        //IEnumerable<TModel> GetListByComplexFilter(PageSearchParameters gridParameters, params Expression<Func<TModel, object>>[] includes);
        //IEnumerable<TModel> GetListByComplexFilter(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression, params Expression<Func<TModel, object>>[] includes);
        IEnumerable<Sort> BuildSortList();
    }
}
