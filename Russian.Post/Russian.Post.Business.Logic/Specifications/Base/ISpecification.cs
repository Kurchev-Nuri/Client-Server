using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Russian.Post.Business.Logic.Specifications.Base
{
    public interface ISpecification<TEntity>
         where TEntity : class
    {
        Expression<Func<TEntity, bool>> Criteria { get; }

        List<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>> Orderes { get; }

        List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; }
    }
}
