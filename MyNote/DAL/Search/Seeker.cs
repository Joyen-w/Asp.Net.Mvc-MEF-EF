using DAL.Dal;
using DAL.WebGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Search
{
    public class Seeker<TModel> where TModel : class
    {
        private readonly IFilterConstraintProvider provider;
        protected ISearchParameters Parameters
        {
            get;
            set;
        }
        public Seeker(ISearchParameters parameters, IFilterConstraintProvider provider)
        {
            this.Parameters = parameters;
            this.provider = provider;
        }
        public string GetSimpleFilterConstraint(out object[] values)
        {
            string text = " 1=1 ";
            values = null;
            if (this.Parameters.Where != null)
            {
                string predicate = SearchConstraintBinder.GetPredicate<TModel>(this.Parameters.Where, out values);
                if (!string.IsNullOrEmpty(predicate) && predicate != "()")
                {
                    text = text + " AND " + predicate;
                }
            }
            return text;
        }
        public string GetComplexFilterConstraint(out object[] values, ref Expression<Func<TModel, bool>> expFilterConstraint)
        {
            string text = " 1=1 ";
            values = null;
            if (this.provider is IAutomaticMatchingFilterConstraint<TModel>)
            {
                if (this.Parameters.Where != null)
                {
                    System.Collections.Generic.IList<object> source = new System.Collections.Generic.List<object>();
                    int num = 0;
                    string text2 = string.Empty;
                    foreach (SearchConstraint current in this.Parameters.Where.Groups)
                    {
                        bool flag = false;
                        Expression<Func<TModel, bool>> expression = null;
                        if (current != null && current.Rules.Count > 0)
                        {
                            foreach (ConstraintRule current2 in current.Rules)
                            {
                                IAutomaticMatchingFilterConstraint<TModel> automaticMatchingFilterConstraint = this.provider as IAutomaticMatchingFilterConstraint<TModel>;
                                Expression<Func<TModel, bool>> expression2 = automaticMatchingFilterConstraint.CustomFilterExpression(current2);
                                if (expression2 != null)
                                {
                                    flag = true;
                                    if (expression == null)
                                    {
                                        expression = expression2;
                                    }
                                    else
                                    {
                                        if (current.GroupOperator.Equals("and", System.StringComparison.OrdinalIgnoreCase))
                                        {
                                            expression.And(expression2);
                                        }
                                        else
                                        {
                                            expression.Or(expression2);
                                        }
                                    }
                                }
                            }
                        }
                        if (flag)
                        {
                            if (expFilterConstraint == null)
                            {
                                expFilterConstraint = expression;
                            }
                            else
                            {
                                if (this.Parameters.Where.GroupOperator.Equals("and", System.StringComparison.OrdinalIgnoreCase))
                                {
                                    expFilterConstraint.And(expression);
                                }
                                else
                                {
                                    expFilterConstraint.Or(expression);
                                }
                            }
                        }
                        else
                        {
                            if (current != null && current.Rules.Count > 0)
                            {
                                text2 = text2 + " AND " + SearchConstraintBinder.GetConstraintByAutomaticFilterConstraint<TModel>(current, ref source, ref num);
                            }
                        }
                    }
                    text += text2;
                    values = source.ToArray<object>();
                }
            }
            else
            {
                if (this.provider is ICustomFilterConstraint<TModel>)
                {
                    ICustomFilterConstraint<TModel> service = this.provider as ICustomFilterConstraint<TModel>;
                    expFilterConstraint = SearchConstraintBinder.GetExpression<TModel>(this.Parameters.Where, service);
                }
                else
                {
                    if (this.Parameters.Where != null)
                    {
                        string text2 = SearchConstraintBinder.GetPredicate<TModel>(this.Parameters.Where, out values);
                        if (!string.IsNullOrEmpty(text2) && text2 != "()")
                        {
                            text = text + " AND " + text2;
                        }
                    }
                }
            }
            return text;
        }
        public string GetOrderConstraint()
        {
            return SearchSortConstraintBinder<TModel>.GetSortConstraint(this.Parameters.SortColumn, this.Parameters.SortOrder);
        }
    }
}
