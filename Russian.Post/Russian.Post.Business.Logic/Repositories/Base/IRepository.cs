using Russian.Post.Business.Logic.Specifications.Base;
using Russian.Post.Common.Results;
using Russian.Post.Database.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Repositories.Base
{
    public interface IRepository<TModel, TEntity>
        where TEntity : class, IDeletedModel
    {
        Task<PostResult<TEntity>> AddAsync(TEntity entity);

        Task<PostResult> DeletedAsync(ISpecification<TEntity> specification);

        Task<IList<TEntity>> AllAsync(ISpecification<TEntity> specification, bool trackable = false);
    }
}
