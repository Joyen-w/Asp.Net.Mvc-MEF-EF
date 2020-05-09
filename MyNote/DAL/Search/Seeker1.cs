using DAL.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Search
{
    public class Seeker1<TModel> where TModel : class
    {
        private readonly Func<ConstraintRule, Expression<Func<TModel, bool>>> getCustomFilterExpression;
        protected ISearchParameters Parameters { get; set; }
        public Seeker1(ISearchParameters parameters, Func<ConstraintRule, Expression<Func<TModel, bool>>> getCustomFilterExpression = null)
        {
            this.Parameters = parameters;
            this.getCustomFilterExpression = getCustomFilterExpression;
        }
        public string GetSimpleFilterConstraint(out object[] arrFilter)
        {
            string filterStr = " 1=1 ";
            arrFilter = null;
            if (this.Parameters.Where != null)
            {
                string predicate = SearchConstraintBinder.GetPredicate<TModel>(this.Parameters.Where, out arrFilter);
                if (!string.IsNullOrEmpty(predicate) && predicate != "()")
                {
                    filterStr = filterStr + " AND " + predicate;
                }
            }
            return filterStr;
        }
        public string GetAutomaticMatchingFilterConstraint(out object[] arrFilter, ref Expression<Func<TModel, bool>> expFilterConstraint)
        {
            string resultStr = " 1=1 ";
            arrFilter = null;
            if (this.Parameters.Where != null)
            {
                IList<object> source = new List<object>();
                int num = 0;
                string filterStr = string.Empty;
                foreach (SearchConstraint searchVo in this.Parameters.Where.Groups)
                {
                    bool flg = false;
                    Expression<Func<TModel, bool>> exp = null;
                    if (searchVo != null && searchVo.Rules.Count > 0)
                    {
                        foreach (ConstraintRule ruleVo in searchVo.Rules)
                        {
                            if (this.getCustomFilterExpression != null)
                            {
                                Expression<Func<TModel, bool>> expVo = this.getCustomFilterExpression(ruleVo);
                                if (expVo != null)
                                {
                                    flg = true;
                                    if (exp == null)
                                    {
                                        exp = expVo;
                                    }
                                    else
                                    {
                                        if (searchVo.GroupOperator.Equals("and", StringComparison.OrdinalIgnoreCase))
                                        {
                                            exp.And(expVo);
                                        }
                                        else
                                        {
                                            exp.Or(expVo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (flg)
                    {
                        if (expFilterConstraint == null)
                        {
                            expFilterConstraint = exp;
                        }
                        else
                        {
                            if (this.Parameters.Where.GroupOperator.Equals("and", System.StringComparison.OrdinalIgnoreCase))
                            {
                                expFilterConstraint.And(exp);
                            }
                            else
                            {
                                expFilterConstraint.Or(exp);
                            }
                        }
                    }
                    else
                    {
                        if (searchVo != null && searchVo.Rules.Count > 0)
                        {
                            filterStr = filterStr + " AND " + SearchConstraintBinder.GetConstraintByAutomaticFilterConstraint<TModel>(searchVo, ref source, ref num);
                        }
                    }
                }
                resultStr += filterStr;
                arrFilter = source.ToArray<object>();
            }
            return resultStr;
        }
        public Expression<Func<TModel, bool>> GetCustomFilterConstraint()
        {
            Expression<Func<TModel, bool>> result;
            if (this.getCustomFilterExpression != null)
            {
                result = null;
            }
            else
            {
                Expression<Func<TModel, bool>> expression = SearchConstraintBinder.GetExpression<TModel>(this.Parameters.Where, this.getCustomFilterExpression);
                result = expression;
            }
            return result;
        }
        public string GetOrderConstraint()
        {
            return SearchSortConstraintBinder<TModel>.GetSortConstraint(this.Parameters.SortColumn, this.Parameters.SortOrder);
        }
    }
}
