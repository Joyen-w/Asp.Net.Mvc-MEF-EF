using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using DAL.Annotations;
using DAL.DBContext;
using DAL.Mvc;
using DAL.Search;
using DAL.Utilities;
using DAL.Web;
using DAL.WebGrid;

namespace DAL.Dal
{
    public class BaseLogic<TModel, TRepository> : ILogic<TModel> where TModel : class where TRepository : IRepository<TModel>, new()
    {
        public TRepository Repository { get; set; }
        protected ModelTypeData ModelTypeData { get; set; }
        public BaseLogic()
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
            this.ModelTypeData = CachedModelTypeData.GetModelTypeData(typeof(TModel));
        }
        public virtual int Count()
        {
            TRepository repository = this.Repository;
            return repository.Count();
        }
        public virtual int Count(Expression<Func<TModel, bool>> where)
        {
            TRepository repository = this.Repository;
            return repository.Count(where);
        }
        public virtual IQueryable<TModel> GetAll()
        {
            TRepository repository = this.Repository;
            return repository.GetAll();
        }
        public virtual IQueryable<TModel> GetAll(bool createProxy)
        {
            TRepository repository = this.Repository;
            return repository.GetAll(createProxy);
        }
        public virtual bool Exists(Expression<Func<TModel, bool>> predicate)
        {
            TRepository repository = this.Repository;
            return repository.Any(predicate);
        }
        public virtual bool RemoteExists(TModel entity)
        {
            return true;
            //int num = 0;
            //string text = string.Empty;
            //string text2 = string.Empty;
            //string text3 = string.Empty;
            //string otherPropertyName = string.Empty;
            //int num2 = 0;
            //PropertyInfo[] properties = entity.GetType().GetProperties();
            //using (IEnumerator<PropertyInfo> enumerator = (
            //    from propertyInfo in properties
            //    where propertyInfo.GetCustomAttributes(false) != null
            //    select propertyInfo).GetEnumerator())
            //{
            //    if (enumerator.MoveNext())
            //    {
            //        PropertyInfo current = enumerator.Current;
            //        num = Convert.ToInt32(current.GetValue(entity));
            //        text = current.Name;
            //    }
            //}
            //using (IEnumerator<PropertyInfo> enumerator = (
            //    from propertyInfo in properties
            //    where propertyInfo.GetCustomAttributes(false) != null
            //    select propertyInfo).GetEnumerator())
            //{
            //    if (enumerator.MoveNext())
            //    {
            //        PropertyInfo current = enumerator.Current;
            //        text3 = current.GetValue(entity).ToString();
            //        text2 = current.Name;
            //        string otherProperty = current.GetCustomAttribute(false).OtherProperty;
            //        if (!string.IsNullOrEmpty(otherProperty) && otherProperty != text)
            //        {
            //            otherPropertyName = otherProperty;
            //            PropertyInfo propertyInfo2 = properties.FirstOrDefault((PropertyInfo p) => p.Name == otherPropertyName);
            //            if (propertyInfo2 != null)
            //            {
            //                num2 = Convert.ToInt32(propertyInfo2.GetValue(entity).ToString());
            //            }
            //        }
            //    }
            //}
            //if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
            //{
            //    throw new InvalidOperationException("主键属性或远程验证属性为空，无法进行远程验证。");
            //}
            //bool result;
            //if (string.IsNullOrEmpty(otherPropertyName))
            //{
            //    TRepository repository = this.Repository;
            //    result = repository.Any(string.Format("{0} == @0 and {1} != @1 ", text2, text), new object[]
            //    {
            //        text3,
            //        num
            //    });
            //}
            //else
            //{
            //    TRepository repository = this.Repository;
            //    result = repository.Any(string.Format("{0} == @0 and {1} == @1 and {2} != @2", otherPropertyName, text2, text), new object[]
            //    {
            //        num2,
            //        text3,
            //        num
            //    });
            //}
            //return result;
        }
        public virtual TModel GetEntity(int id)
        {
            TRepository repository = this.Repository;
            return repository.Get(new object[] { id });
        }
        public virtual TModel GetEntity(Expression<Func<TModel, bool>> where)
        {
            TRepository repository = this.Repository;
            return repository.Get(where);
        }
        public virtual IEnumerable<TModel> GetMany(Expression<Func<TModel, bool>> where)
        {
            TRepository repository = this.Repository;
            return repository.GetMany(where);
        }
        //public virtual IEnumerable<TModel> GetMany(Expression<Func<TModel, bool>> where, string orderBy)
        //{
        //    TRepository repository = this.Repository;
        //    return repository.GetMany(where, orderBy);
        //}
        //public virtual IEnumerable<TModel> GetMany(int outputCount, Expression<Func<TModel, bool>> where, string orderBy)
        //{
        //    TRepository repository = this.Repository;
        //    return repository.GetMany(where, orderBy, outputCount);
        //}
        public virtual IEnumerable<TModel> GetMany(ISearchParameters searchParameters)
        {
            SearchEngine<TModel, TRepository> searchEngine = new SearchEngine<TModel, TRepository>();
            return searchEngine.GetMany(searchParameters, 0);
        }
        public virtual void Add(TModel entity)
        {
            TRepository repository = this.Repository;
            repository.Add(entity);
            this.Save();
        }
        public virtual void Delete(int id)
        {
            TRepository repository = this.Repository;
            TModel entity = repository.Get(new object[]
            {
                id
            });
            repository = this.Repository;
            repository.Delete(entity);
            this.Save();
        }
        public virtual void BatchDelete(ICollection<int> ids)
        {
            TRepository repository = this.Repository;
            repository.BatchDelete(ids);
            this.Save();
        }
        public virtual void BatchDelete(Expression<Func<TModel, bool>> where)
        {
            TRepository repository = this.Repository;
            repository.BatchDelete(where);
            this.Save();
        }

        public virtual void DeleteAll()
        {
            TRepository repository = this.Repository;
            repository.DeleteAll();
        }
        public virtual void Update(TModel entity)
        {
            TRepository repository = this.Repository;
            repository.Update(entity);
            this.Save();
        }
        public virtual void Modify(TModel entity)
        {
            TRepository repository = this.Repository;
            repository.Modify(entity);
            this.Save();
        }
        public virtual void BatchUpdate(Expression<Func<TModel, bool>> where, Expression<Func<TModel, TModel>> update)
        {
            TRepository repository = this.Repository;
            repository.BatchUpdate(where, update);
            this.Save();
        }
        public virtual void BatchUpdataSort(ICollection<TModel> models)
        {
            TRepository repository = this.Repository;
            repository.BatchUpdataSort(models);
        }
        public virtual void Save()
        {
            TRepository repository = this.Repository;
            repository.Save();
        }

        public virtual IEnumerable<SelectListItem> GetSelectListItems()
        {
            return this.GetAll().BuildList((TModel x) => this.ModelTypeData.GetDisplayColumnValue(x), (TModel x) => this.ModelTypeData.GetIdValue(x), null);
        }
        public virtual IEnumerable<SelectListItem> GetSelectListItems(Expression<Func<TModel, bool>> where)
        {
            return this.GetMany(where).BuildList((TModel x) => this.ModelTypeData.GetDisplayColumnValue(x), (TModel x) => this.ModelTypeData.GetIdValue(x), null);
        }
        //public virtual IPagedList<TModel> GetPagedList(PageSearchParameters gridParameters)
        //{
        //    return this.GetPagedList(gridParameters, null);
        //}
        //public virtual IPagedList<TModel> GetPagedList(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression)
        //{
        //    Check.NotNull(gridParameters, "gridParameters");
        //    Seeker<TModel> seeker = new Seeker<TModel>(gridParameters, this as IFilterConstraintProvider);
        //    object[] whereParameters;
        //    string simpleFilterConstraint = seeker.GetSimpleFilterConstraint(out whereParameters);
        //    string orderConstraint = seeker.GetOrderConstraint();
        //    TRepository repository = this.Repository;
        //    return repository.GetPagedList(gridParameters.PageIndex, gridParameters.PageSize, simpleFilterConstraint, whereParameters, expression, orderConstraint);
        //}
        //public virtual IPagedList<TModel> GetPagedListByComplexFilter(PageSearchParameters gridParameters)
        //{
        //    return this.GetPagedListByComplexFilter(gridParameters, null, null);
        //}
        //public virtual IPagedList<TModel> GetPagedListByComplexFilter(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression)
        //{
        //    return this.GetPagedListByComplexFilter(gridParameters, expression, null);
        //}
        //public virtual IPagedList<TModel> GetPagedListByComplexFilter(PageSearchParameters gridParameters, params Expression<Func<TModel, object>>[] includes)
        //{
        //    return this.GetPagedListByComplexFilter(gridParameters, null, includes);
        //}
        //public virtual IPagedList<TModel> GetPagedListByComplexFilter(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression, params Expression<Func<TModel, object>>[] includes)
        //{
        //    Expression<Func<TModel, bool>> expression2 = null;
        //    Seeker<TModel> seeker = new Seeker<TModel>(gridParameters, this as IFilterConstraintProvider);
        //    object[] whereParameters;
        //    string complexFilterConstraint = seeker.GetComplexFilterConstraint(out whereParameters, ref expression2);
        //    if (expression != null)
        //    {
        //        expression2 = ((expression2 == null) ? expression : expression2.And(expression));
        //    }
        //    string orderConstraint = seeker.GetOrderConstraint();
        //    IPagedList<TModel> pagedList;
        //    if (includes == null || includes.Length == 0)
        //    {
        //        TRepository repository = this.Repository;
        //        pagedList = repository.GetPagedList(gridParameters.PageIndex, gridParameters.PageSize, complexFilterConstraint, whereParameters, expression2, orderConstraint);
        //    }
        //    else
        //    {
        //        TRepository repository = this.Repository;
        //        pagedList = repository.GetPagedList(gridParameters.PageIndex, gridParameters.PageSize, complexFilterConstraint, whereParameters, expression2, orderConstraint, includes);
        //    }
        //    return pagedList;
        //}
        //public virtual IEnumerable<TModel> GetList(PageSearchParameters gridParameters)
        //{
        //    return this.GetList(gridParameters, null);
        //}
        //public virtual IEnumerable<TModel> GetList(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression)
        //{
        //    Seeker<TModel> seeker = new Seeker<TModel>(gridParameters, this as IFilterConstraintProvider);
        //    object[] whereParameters;
        //    string simpleFilterConstraint = seeker.GetSimpleFilterConstraint(out whereParameters);
        //    string orderConstraint = seeker.GetOrderConstraint();
        //    TRepository repository = this.Repository;
        //    return repository.GetMany(simpleFilterConstraint, whereParameters, expression, orderConstraint);
        //}
        //public virtual IEnumerable<TModel> GetListByComplexFilter(PageSearchParameters gridParameters)
        //{
        //    return this.GetListByComplexFilter(gridParameters, null, null);
        //}
        //public virtual IEnumerable<TModel> GetListByComplexFilter(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression)
        //{
        //    return this.GetListByComplexFilter(gridParameters, expression, null);
        //}
        //public virtual IEnumerable<TModel> GetListByComplexFilter(PageSearchParameters gridParameters, params Expression<Func<TModel, object>>[] includes)
        //{
        //    return this.GetListByComplexFilter(gridParameters, null, includes);
        //}
        //public virtual IEnumerable<TModel> GetListByComplexFilter(PageSearchParameters gridParameters, Expression<Func<TModel, bool>> expression, params Expression<Func<TModel, object>>[] includes)
        //{
        //    Seeker<TModel> seeker = new Seeker<TModel>(gridParameters, this as IFilterConstraintProvider);
        //    Expression<Func<TModel, bool>> expression2 = null;
        //    object[] whereParameters;
        //    string complexFilterConstraint = seeker.GetComplexFilterConstraint(out whereParameters, ref expression2);
        //    if (expression != null)
        //    {
        //        expression2 = ((expression2 == null) ? expression : expression2.And(expression));
        //    }
        //    string orderConstraint = seeker.GetOrderConstraint();
        //    IEnumerable<TModel> many;
        //    if (includes == null || includes.Length == 0)
        //    {
        //        TRepository repository = this.Repository;
        //        many = repository.GetMany(complexFilterConstraint, whereParameters, expression2, orderConstraint);
        //    }
        //    else
        //    {
        //        TRepository repository = this.Repository;
        //        many = repository.GetMany(complexFilterConstraint, whereParameters, expression2, orderConstraint, includes);
        //    }
        //    return many;
        //}
        public virtual IEnumerable<Sort> BuildSortList()
        {
            TRepository repository = this.Repository;
            return repository.GetAll().BuildSortList((TModel x) => this.ModelTypeData.GetIdValue(x), (TModel x) => this.ModelTypeData.GetDisplayColumnValue(x));
        }
        protected virtual int GetPrimaryKeyId(TModel model)
        {
            PropertyInfo propertyInfo = model.GetType().GetProperties().FirstOrDefault((PropertyInfo p) => p.GetCustomAttributes(typeof(IdAttribute), false).Any());
            return (int)propertyInfo.GetValue(model);
        }

        public void BatchDelete(ICollection<int> ids, Func<TModel, int> keyExpression)
        {
            throw new NotImplementedException();
        }
    }
}
