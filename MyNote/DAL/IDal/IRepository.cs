using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.DBContext;

namespace DAL.Dal
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> DbSet { get; set; }
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Modify(TEntity entity);
        void BatchUpdate(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> update);
        void BatchUpdataSort(ICollection<TEntity> entitys);
        void BatchDelete(Expression<Func<TEntity, bool>> predicate);
        void BatchDelete(ICollection<int> ids);
        void DeleteAll();
        void SetDetached(object entity);
        void SetUnchanged(object entity);
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        bool Any(string predicate, params object[] values);
        TEntity Get(params object[] keyValues);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        //TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetAll(bool proxyCreationEnabled);
        void ApplyCurrentValues(TEntity stateEntry, TEntity currentEntity);
        void ApplyComplexValues<T, TKey>(ICollection<T> first, ICollection<T> second, Func<T, TKey> getKey);
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
        //IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, string orderBy, params Expression<Func<TEntity, object>>[] includes);
        //IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, string orderBy, int totalNumber, params Expression<Func<TEntity, object>>[] includes);
        //IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, string orderBy);
        //IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, string orderBy, int totalNumber);
        //IQueryable<TEntity> GetMany(string where, object[] whereParameters, string orderBy);
        //IQueryable<TEntity> GetMany(string where, object[] whereParameters, string orderBy, int totalNumber);
        //IQueryable<TEntity> GetMany(string where, object[] whereParameters, string orderBy, params string[] includes);
        //IQueryable<TEntity> GetMany(string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy);
        //IQueryable<TEntity> GetMany(string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy, int totalNumber);
        //IQueryable<TEntity> GetMany(string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy, params Expression<Func<TEntity, object>>[] includes);
        //IQueryable<TEntity> GetMany(string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy, int totalNumber, params Expression<Func<TEntity, object>>[] includes);
        //IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, IOrderByExpression<TEntity> orderBy);
        //IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, IOrderByExpression<TEntity> orderBy, params Expression<Func<TEntity, object>>[] includes);
        //IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, System.Collections.Generic.IEnumerable<IOrderByExpression<TEntity>> orderBy);
        //IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, System.Collections.Generic.IEnumerable<IOrderByExpression<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includes);
        //IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, string orderBy);
        //IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, string orderBy, params Expression<Func<TEntity, object>>[] includes);
        //IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, string where, object[] whereParameters, string orderBy);
        //IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, string where, object[] whereParameters, string orderBy, params string[] includes);
        //IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy);
        //IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy, params Expression<Func<TEntity, object>>[] includes);
        void Save();
    }
}
