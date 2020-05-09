using DAL.Annotations;
using DAL.Utilities;
using DAL.Web;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dal
{
    public class GenericRepository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class where TDbContext : DbContext, new()
    {
        public DbSet<TEntity> DbSet{get;set;}
        public TDbContext DbContext{get;set;}
        protected virtual IQueryable<TEntity> Selector
        {
            get{return this.DbSet;}
        }
        public GenericRepository()
        {
            this.DbContext = Activator.CreateInstance<TDbContext>();
            TDbContext dbContext = this.DbContext;
            this.DbSet = dbContext.Set<TEntity>();
        }
        public virtual void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }
        public virtual void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }
        public virtual void Update(TEntity entity)
        {
            TDbContext dbContext = this.DbContext;
            dbContext.Entry(entity).State =  EntityState.Modified; 
        }
        public virtual void Modify(TEntity entity)
        {
            TDbContext dbContext = this.DbContext;
            DbEntityEntry<TEntity> dbEntityEntry = dbContext.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
            System.Reflection.PropertyInfo[] properties = typeof(TEntity).GetProperties();
            System.Reflection.PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                System.Reflection.PropertyInfo propertyInfo = array[i];
                if (!Attribute.IsDefined(propertyInfo, typeof(NotMappedAttribute)))
                {
                    if (Attribute.IsDefined(propertyInfo, typeof(UnmodifiableAttribute)))
                    {
                        dbEntityEntry.Property(propertyInfo.Name).IsModified = false;
                    }
                }
            }
        }
        public virtual void BatchUpdate(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> update)
        {
            BatchExtensions.Update<TEntity>(this.DbSet.Where(predicate), update);
        }
        public virtual void BatchUpdataSort(ICollection<TEntity> entities)
        {
            if (entities.Any())
            {
                IQueryable<TEntity> all = this.GetAll();
                using (IEnumerator<TEntity> enumerator = entities.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        IOrderable orderable = (IOrderable)enumerator.Current;
                        using (IEnumerator<TEntity> enumerator2 = all.GetEnumerator())
                        {
                            while (enumerator2.MoveNext())
                            {
                                IOrderable orderable2 = (IOrderable)enumerator2.Current;
                                if (orderable2.ComparisonObject(orderable))
                                {
                                    orderable2.Order = orderable.Order;
                                    break;
                                }
                            }
                        }
                    }
                }
                TDbContext dbContext = this.DbContext;
                dbContext.SaveChanges();
            }
        }
        public virtual void BatchDelete(Expression<Func<TEntity, bool>> predicate)
        {
            BatchExtensions.Delete<TEntity>(this.DbSet.Where(predicate));
        }
        public virtual void BatchDelete(ICollection<int> ids)
        {
            if (ids.Any())
            {
                TDbContext dbContext = this.DbContext;
                DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
                System.Reflection.PropertyInfo primaryKeyInfo = ModelHelper.GetPrimaryKeyInfo(typeof(TEntity));
                foreach (int current in ids)
                {
                    foreach (TEntity current2 in dbSet)
                    {
                        int num = (int)current2.GetType().GetProperty(primaryKeyInfo.Name).GetValue(current2);
                        if (current == num)
                        {
                            dbSet.Remove(current2);
                            break;
                        }
                    }
                }
                dbContext = this.DbContext;
                dbContext.SaveChanges();
            }
        }
        public virtual void DeleteAll()
        {
            BatchExtensions.Delete<TEntity>(this.DbSet);
        }
        public virtual void SetDetached(object entity)
        {
            TDbContext dbContext = this.DbContext;
            dbContext.Entry(entity).State = EntityState.Added;
        }
        public virtual void SetUnchanged(object entity)
        {
            TDbContext dbContext = this.DbContext;
            dbContext.Entry(entity).State = EntityState.Deleted;
        }
        public virtual int Count()
        {
            return this.Selector.Count();
        }
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Selector.Count(predicate);
        }
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Selector.Any(predicate);
        }
        public virtual bool Any(string predicate, params object[] values)
        {
            return DynamicQueryable.Where<TEntity>(this.Selector, predicate, values).Any<TEntity>();
        }
        public virtual TEntity Get(params object[] keyValues)
        {
            return this.DbSet.Find(keyValues);
        }
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Selector.Where(predicate).FirstOrDefault();
        }
        //public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    //return Selector.Include().Where(predicate).FirstOrDefault();

        //    //Selector.Setup(set => set.Include(It.IsAny<string>())).Returns(dbSet.Object);
        //    //return Selector.Object;

        //    //return this.Selector.Include<TEntity>((IList<Expression<Func<TEntity, object>>>)includes).Where(predicate).FirstOrDefault();


        //}
        public virtual IQueryable<TEntity> GetAll()
        {
            return this.Selector;
        }
        public virtual IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            //return this.Selector.Include(includes[0]);
            return this.Selector.Include(includes[0]);

        }
        public virtual IQueryable<TEntity> GetAll(bool proxyCreationEnabled)
        {
            TDbContext dbContext = this.DbContext;
            dbContext.Configuration.ProxyCreationEnabled = proxyCreationEnabled ;
            return this.Selector;
        }
        public virtual void ApplyCurrentValues(TEntity stateEntry, TEntity currentEntity)
        {
            TDbContext dbContext = this.DbContext;
            dbContext.Entry(stateEntry).CurrentValues.SetValues(currentEntity);
        }
        public virtual void ApplyComplexValues<T, TKey>(ICollection<T> first, ICollection<T> second, Func<T, TKey> getKey)
        {
            if (first == null)
            {
                first = new Collection<T>();
            }
            if (second == null)
            {
                second = new Collection<T>();
            }
            first.Except(second, getKey).ToList().ForEach(item => first.Remove(item));
            second.Except(first, getKey).ToList().ForEach(delegate (T item) {
                first.Add(item);
                SetUnchanged(item);
            });

        }
        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return this.Selector.Where(where);
        }
        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.Selector.Include(includes[0]).Where(where);
        }
        //public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, string orderBy, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    return DynamicQueryable.OrderBy<TEntity>(this.Selector.Include(includes).Where(where), orderBy, new object[0]);
        //}
        //public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, string orderBy, int totalNumber, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    return DynamicQueryable.OrderBy<TEntity>(this.Selector.Include(includes).Where(where), orderBy, new object[0]).Take(totalNumber);
        //}
        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, string orderBy)
        {
            return DynamicQueryable.OrderBy<TEntity>(this.Selector.Where(where), orderBy, new object[0]);
        }
        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, string orderBy, int totalNumber)
        {
            return DynamicQueryable.OrderBy<TEntity>(this.Selector.Where(where), orderBy, new object[0]).Take(totalNumber);
        }
        public virtual IQueryable<TEntity> GetMany(string where, object[] whereParameters, string orderBy)
        {
            IQueryable<TEntity> result;
            if (orderBy != null)
            {
                result = DynamicQueryable.OrderBy<TEntity>(DynamicQueryable.Where<TEntity>(this.Selector, where, whereParameters), orderBy, new object[0]);
            }
            else
            {
                result = DynamicQueryable.Where<TEntity>(this.Selector, where, whereParameters);
            }
            return result;
        }
        public virtual IQueryable<TEntity> GetMany(string where, object[] whereParameters, string orderBy, int totalNumber)
        {
            return DynamicQueryable.OrderBy<TEntity>(DynamicQueryable.Where<TEntity>(this.Selector, where, whereParameters), orderBy, new object[0]).Take(totalNumber);
        }
        //public virtual IQueryable<TEntity> GetMany(string where, object[] whereParameters, string orderBy, string[] includes)
        //{
        //    return DynamicQueryable.OrderBy<TEntity>(DynamicQueryable.Where<TEntity>(this.Selector.Include(includes), where, whereParameters), orderBy, new object[0]);
        //}
        public virtual IQueryable<TEntity> GetMany(string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy)
        {
            IQueryable<TEntity> queryable = DynamicQueryable.Where<TEntity>(this.Selector, where1, whereParameters);
            if (where2 != null)
            {
                queryable = queryable.Where(where2);
            }
            return DynamicQueryable.OrderBy<TEntity>(queryable, orderBy, new object[0]);
        }
        public virtual IQueryable<TEntity> GetMany(string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy, int totalNumber)
        {
            IQueryable<TEntity> queryable = DynamicQueryable.Where<TEntity>(this.Selector, where1, whereParameters);
            if (where2 != null)
            {
                queryable = queryable.Where(where2);
            }
            return DynamicQueryable.OrderBy<TEntity>(queryable, orderBy, new object[0]).Take(totalNumber);
        }
        //public virtual IQueryable<TEntity> GetMany(string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    IQueryable<TEntity> queryable = DynamicQueryable.Where<TEntity>(this.Selector.Include(includes), where1, whereParameters);
        //    if (where2 != null)
        //    {
        //        queryable = queryable.Where(where2);
        //    }
        //    return DynamicQueryable.OrderBy<TEntity>(queryable, orderBy, new object[0]);
        //}
        //public virtual IQueryable<TEntity> GetMany(string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy, int totalNumber, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    IQueryable<TEntity> queryable = DynamicQueryable.Where<TEntity>(this.Selector.Include(includes), where1, whereParameters);
        //    if (where2 != null)
        //    {
        //        queryable = queryable.Where(where2);
        //    }
        //    return DynamicQueryable.OrderBy<TEntity>(queryable, orderBy, new object[0]).Take(totalNumber);
        //}
        //public virtual IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, IOrderByExpression<TEntity> orderBy)
        //{
        //    return this.Selector.Where(where).OrderBy(orderBy).ToPagedList(pageNumber, pageSize);
        //}
        //public virtual IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, IOrderByExpression<TEntity> orderBy, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    return this.Selector.Include(includes).Where(where).OrderBy(orderBy).ToPagedList(pageNumber, pageSize);
        //}
        //public virtual IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, IEnumerable<IOrderByExpression<TEntity>> orderBy)
        //{
        //    return this.Selector.Where(where).OrderBy(orderBy).ToPagedList(pageNumber, pageSize);
        //}
        //public virtual IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, IEnumerable<IOrderByExpression<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    return this.Selector.Include(includes).Where(where).OrderBy(orderBy).ToPagedList(pageNumber, pageSize);
        //}
        //public virtual IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, string orderBy)
        //{
        //    return DynamicQueryable.OrderBy<TEntity>(this.Selector.Where(where), orderBy, new object[0]).ToPagedList(pageNumber, pageSize);
        //}
        //public virtual IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, string orderBy, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    return DynamicQueryable.OrderBy<TEntity>(this.Selector.Include(includes).Where(where), orderBy, new object[0]).ToPagedList(pageNumber, pageSize);
        //}
        //public virtual IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, string where, object[] whereParameters, string orderBy)
        //{
        //    return DynamicQueryable.OrderBy<TEntity>(DynamicQueryable.Where<TEntity>(this.Selector, where, whereParameters), orderBy, new object[0]).ToPagedList(pageNumber, pageSize);
        //}
        //public virtual IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, string where, object[] whereParameters, string orderBy, params string[] includes)
        //{
        //    return DynamicQueryable.OrderBy<TEntity>(DynamicQueryable.Where<TEntity>(this.Selector.Include(includes), where, whereParameters), orderBy, new object[0]).ToPagedList(pageNumber, pageSize);
        //}
        //public virtual IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy)
        //{
        //    IQueryable<TEntity> queryable = DynamicQueryable.Where<TEntity>(this.Selector, where1, whereParameters);
        //    if (where2 != null)
        //    {
        //        queryable = queryable.Where(where2);
        //    }
        //    return DynamicQueryable.OrderBy<TEntity>(queryable, orderBy, new object[0]).ToPagedList(pageNumber, pageSize);
        //}
        //public virtual IPagedList<TEntity> GetPagedList(int pageNumber, int pageSize, string where1, object[] whereParameters, Expression<Func<TEntity, bool>> where2, string orderBy, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    IQueryable<TEntity> queryable = DynamicQueryable.Where<TEntity>(this.Selector.Include(includes), where1, whereParameters);
        //    if (where2 != null)
        //    {
        //        queryable = queryable.Where(where2);
        //    }
        //    return DynamicQueryable.OrderBy<TEntity>(queryable, orderBy, new object[0]).ToPagedList(pageNumber, pageSize);
        //}
        public virtual void Save()
        {
            TDbContext dbContext = this.DbContext;
            string text = dbContext.GetValidationErrors().SelectMany((DbEntityValidationResult errItem) => errItem.ValidationErrors).Aggregate(string.Empty, (string current, DbValidationError err) => current + err.ErrorMessage + Environment.NewLine);
            if (!string.IsNullOrEmpty(text))
            {
                throw new Exception(text);
            }
            dbContext = this.DbContext;
            dbContext.SaveChanges();
        }
    }
}
