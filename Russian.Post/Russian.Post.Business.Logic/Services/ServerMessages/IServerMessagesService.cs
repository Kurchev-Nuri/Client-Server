using Russian.Post.Common.Results;
using Russian.Post.Forms;
using Russian.Post.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Services.ServerMessages
{
    public interface IServerMessagesService
    {
        Task<IList<ServerMessage>> AllMessages();

        Task<PostResult<ServerMessage>> AddMessage(AddMessageForm form);
    }
}
