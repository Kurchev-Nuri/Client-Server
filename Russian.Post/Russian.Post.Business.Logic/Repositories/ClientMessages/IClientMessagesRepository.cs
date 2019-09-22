using Russian.Post.Business.Logic.Repositories.Base;
using Russian.Post.Database.Models;
using Russian.Post.Models;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Repositories.ClientMessages
{
    public interface IClientMessagesRepository : IRepository<ClientMessage, PostClientMessage>
    {
        Task<int> SaveChangesAsync();
    }
}
