using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Russian.Post.Business.Logic.Specifications.Base;
using Russian.Post.Common.Options;
using Russian.Post.Common.Results;
using Russian.Post.Database.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Repositories.Base
{
    internal abstract class BaseRepository<TContext, TModel, TEntity> : IRepository<TModel, TEntity>, IDisposable
       where TContext : DbContext
       where TEntity : class, IDeletedModel
    {
        private bool _disposed = false;
        private readonly IOptionsMonitor<RepositoryOptions> _options;

        protected BaseRepository(IMapper mapper, TContext context, IOptionsMonitor<RepositoryOptions> options)
        {
            Mapper = mapper;
            Context = context;
            _options = options;
        }

        ~BaseRepository()
        {
            Dispose(false);
        }

        protected IMapper Mapper { get; }

        protected TContext Context { get; }

        private RepositoryOptions Options => _options.CurrentValue ?? RepositoryOptions.Default;

        protected abstract Task SaveAsync();

        public async Task<PostResult<TModel>> AddAsync(TEntity entity)
        {
            if (entity == default)
                return PostResult<TModel>.WithError(PostErrorCodes.InvalidInput);

            var result = Context.Set<TEntity>()
                .Add(entity);

            if (Options.AutoSaveEnabled)
                await SaveAsync();

            return new PostResult<TModel>(Mapper.Map<TModel>(result.Entity));
        }

        public async Task<PostResult> DeletedAsync(ISpecification<TEntity> specification)
        {
            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(specification.Criteria)
                .ConfigureAwait(false);

            if (entity == default)
                return PostResult.WithError(PostErrorCodes.EntityWasNotFound);

            entity.IsDeleted = true;

            if (Options.AutoSaveEnabled)
                await SaveAsync();

            return PostResult.Default;
        }

        public async Task<IList<TModel>> AllAsync(ISpecification<TEntity> specification, bool trackable = false)
        {
            var query = Context.Set<TEntity>()
               .AsQueryable();

            if (!trackable)
                query = query.AsNoTracking();

            query = specification.Includes.Aggregate(query, (querable, includeTo) => querable = includeTo(querable));
            query = specification.Orderes.Aggregate(query, (querable, includeTo) => querable = includeTo(querable));

            var result = await query.Where(specification.Criteria)
                .ToListAsync()
                .ConfigureAwait(false);

            return result.Select(Mapper.Map<TModel>).ToList();
        }

        public async Task<PostResult<TEntity>> Update(TEntity entity)
        {
            if (entity == default)
                return PostResult<TEntity>.WithError(PostErrorCodes.InvalidInput);

            Context.Entry(entity).State = EntityState.Modified;

            if (Options.AutoSaveEnabled)
                await SaveAsync();

            return new PostResult<TEntity>(entity);
        }

        public async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification, bool trackable = false)
        {
            var query = Context.Set<TEntity>()
                .AsQueryable();

            if (!trackable)
                query = query.AsNoTracking();

            query = specification.Includes.Aggregate(query, (querable, includeTo) => querable = includeTo(querable));

            return await query.FirstOrDefaultAsync(specification.Criteria)
                .ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                Context.Dispose();

            _disposed = true;
        }
    }
}
