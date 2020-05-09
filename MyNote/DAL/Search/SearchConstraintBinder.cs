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
    public static class SearchConstraintBinder
    {
        public static Expression<Func<T, bool>> GetExpression<T>(SearchConstraint gridConstraint, Func<ConstraintRule, Expression<Func<T, bool>>> getCustomFilterExpression)
        {
            Expression<Func<T, bool>> expression = PredicateBuilder.True<T>();
            if (gridConstraint != null)
            {
                foreach (SearchConstraint current in gridConstraint.Groups)
                {
                    if (current != null)
                    {
                        Expression<Func<T, bool>> expression2 = null;
                        foreach (ConstraintRule current2 in current.Rules)
                        {
                            Expression<Func<T, bool>> expression3 = getCustomFilterExpression(current2);
                            if (expression3 != null)
                            {
                                if (expression2 == null)
                                {
                                    expression2 = expression3;
                                }
                                else
                                {
                                    if (current.GroupOperator.Equals("and", StringComparison.OrdinalIgnoreCase))
                                    {
                                        expression2.And(expression3);
                                    }
                                    else
                                    {
                                        expression2.Or(expression3);
                                    }
                                }
                            }
                        }
                        expression = (gridConstraint.GroupOperator.Equals("and", StringComparison.OrdinalIgnoreCase) ? expression.And(expression2) : expression.Or(expression2));
                    }
                }
            }
            return expression;
        }
        public static Expression<Func<T, bool>> GetExpression<T>(SearchConstraint gridConstraint, ICustomFilterConstraint<T> service)
        {
            Expression<Func<T, bool>> expression = PredicateBuilder.True<T>();
            if (gridConstraint != null)
            {
                foreach (SearchConstraint current in gridConstraint.Groups)
                {
                    if (current != null)
                    {
                        Expression<Func<T, bool>> expression2 = null;
                        foreach (ConstraintRule current2 in current.Rules)
                        {
                            Expression<Func<T, bool>> expression3 = service.CustomFilterExpression(current2);
                            if (expression3 != null)
                            {
                                if (expression2 == null)
                                {
                                    expression2 = expression3;
                                }
                                else
                                {
                                    if (current.GroupOperator.Equals("and", StringComparison.OrdinalIgnoreCase))
                                    {
                                        expression2.And(expression3);
                                    }
                                    else
                                    {
                                        expression2.Or(expression3);
                                    }
                                }
                            }
                        }
                        expression = (gridConstraint.GroupOperator.Equals("and", StringComparison.OrdinalIgnoreCase) ? expression.And(expression2) : expression.Or(expression2));
                    }
                }
            }
            return expression;
        }
        public static string GetConstraintByAutomaticFilterConstraint<T>(SearchConstraint group, ref IList<object> values, ref int parameterIndex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("(");
            List<ConstraintRule> rules = group.Rules;
            if (rules != null && rules.Count > 0)
            {
                System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();
                int num = 1;
                using (List<ConstraintRule>.Enumerator enumerator = rules.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ConstraintRule rule = enumerator.Current;
                        if (!string.IsNullOrEmpty(rule.Field))
                        {
                            System.Reflection.PropertyInfo propertyInfo = (
                                from p in properties
                                where p.Name.ToLower() == rule.Field.ToLower()
                                select p).FirstOrDefault();
                            if (!(propertyInfo == null))
                            {
                                Type propertyType = propertyInfo.PropertyType;
                                values.Add(rule.GetValue(propertyType));
                                stringBuilder.Append(rule.GetPredicate(parameterIndex));
                                if (num < rules.Count)
                                {
                                    stringBuilder.Append(group.GroupOperator.Equals("and", StringComparison.OrdinalIgnoreCase) ? " AND " : " OR ");
                                }
                                parameterIndex++;
                                num++;
                            }
                        }
                    }
                }
            }
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }
        public static string GetPredicate<T>(SearchConstraint gridConstraint, out object[] values)
        {
            IList<object> list = new List<object>();
            int num = 0;
            string predicate = GetPredicate<T>(gridConstraint, list, ref num);
            values = list.ToArray<object>();
            return predicate;
        }
        private static string GetPredicate<T>(SearchConstraint constraint, IList<object> values, ref int parameterIndex)
        {
            StringBuilder strBuilder = new StringBuilder();
            bool flg = !string.IsNullOrEmpty(constraint.GroupOperator) && ((constraint.Groups != null) ? constraint.Groups.Count : 0) + ((constraint.Rules != null) ? constraint.Rules.Count : 0) > 1;
            if (flg)
            {
                strBuilder.Append("(");
            }
            if (constraint.Rules != null && constraint.Rules.Count > 0)
            {
                System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();
                int num = 1;
                using (List<ConstraintRule>.Enumerator enumerator = constraint.Rules.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ConstraintRule rule = enumerator.Current;
                        if (!string.IsNullOrEmpty(rule.Field))
                        {
                            System.Reflection.PropertyInfo propertyInfo = (
                                from p in properties
                                where p.Name.ToLower() == rule.Field.ToLower()
                                select p).FirstOrDefault();
                            if (!(propertyInfo == null))
                            {
                                Type propertyType = propertyInfo.PropertyType;
                                values.Add((propertyType == typeof(string)) ? rule.GetValue(propertyType).ToString().ToLower() : rule.GetValue(propertyType));
                                strBuilder.Append(rule.GetPredicate(parameterIndex));
                                if (num < constraint.Rules.Count && !string.IsNullOrEmpty(constraint.Rules[num].Field))
                                {
                                    strBuilder.Append(" " + constraint.GroupOperator + " ");
                                }
                                parameterIndex++;
                                num++;
                            }
                        }
                    }
                }
            }
            if (constraint.Groups != null && constraint.Groups.Count > 0)
            {
                int num2 = 0;
                foreach (SearchConstraint current in constraint.Groups)
                {
                    num2++;
                    if (current != null)
                    {
                        if (current.Rules != null && current.Rules.Count > 0)
                        {
                            if (num2 > 1)
                            {
                                strBuilder.Append(" " + constraint.GroupOperator + " ");
                            }
                            strBuilder.Append(GetPredicate<T>(current, values, ref parameterIndex));
                        }
                    }
                }
            }
            if (flg)
            {
                strBuilder.Append(")");
            }
            return strBuilder.ToString();
        }
    }
}
