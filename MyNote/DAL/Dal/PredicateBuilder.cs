using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dal
{
    public static class PredicateBuilder
    {
        public class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;
            public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = (map ?? new Dictionary<ParameterExpression, ParameterExpression>());
            }
            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }
            protected override Expression VisitParameter(ParameterExpression node)
            {
                ParameterExpression parameterExpression;
                if (this.map.TryGetValue(node, out parameterExpression))
                {
                    node = parameterExpression;
                }
                return base.VisitParameter(node);
            }
        }
        public static Expression<Func<T, bool>> True<T>()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "param");
            return Expression.Lambda<Func<T, bool>>(Expression.Constant(true, typeof(bool)), new ParameterExpression[]
            {
                parameterExpression
            });
        }
        public static Expression<Func<T, bool>> False<T>()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "param");
            return Expression.Lambda<Func<T, bool>>(Expression.Constant(false, typeof(bool)), new ParameterExpression[]
            {
                parameterExpression
            });
        }
        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate)
        {
            return predicate;
        }
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, new Func<Expression, Expression, Expression>(Expression.AndAlso));
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, new Func<Expression, Expression, Expression>(Expression.OrElse));
        }
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            UnaryExpression body = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(body, expression.Parameters);
        }
        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            Dictionary<ParameterExpression, ParameterExpression> map = first.Parameters.Select((ParameterExpression f, int i) => new
            {
                f,
                s = second.Parameters[i]
            }).ToDictionary(p => p.s, p => p.f);
            Expression arg = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, arg), first.Parameters);
        }
    }
}
