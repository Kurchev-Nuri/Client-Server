using AutoMapper;
using Microsoft.Extensions.Options;
using Russian.Post.Business.Logic.Repositories.Base;
using Russian.Post.Common.Options;
using Russian.Post.Database.Contexts;
using Russian.Post.Database.Models;
using Russian.Post.Models;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Repositories.ClientMessages
{
    internal sealed class LocalClientMessagesRepository : BaseRepository<RussianPostContext, ClientMessage, PostClientMessage>,
                                                          ILocalClientMessagesRepository
    {
        public LocalClientMessagesRepository(IMapper mapper, RussianPostContext context, IOptionsMonitor<RepositoryOptions> options) 
            : base(mapper, context, options)
        {
        }

        public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync().ConfigureAwait(false);

        protected override Task SaveAsync() => SaveChangesAsync();
    }
}
