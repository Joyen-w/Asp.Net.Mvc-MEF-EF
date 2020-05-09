using DAL.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.WebGrid
{
    public interface ICustomFilterConstraint<TModel> : IFilterConstraintProvider
    {
        Expression<Func<TModel, bool>> CustomFilterExpression(ConstraintRule rule);
    }
}
