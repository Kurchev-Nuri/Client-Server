using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Russian.Post.Business.Logic.Specifications.Base
{
    internal abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : class
    {
        public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
            Orderes = new List<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>();
            Includes = new List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>();
        }

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public List<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>> Orderes { get; }

        public List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; }

        public void AddOrder(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orders) => Orderes.Add(orders);

        public void AddInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include) => Includes.Add(include);
    }
}
