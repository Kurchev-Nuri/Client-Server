using Russian.Post.Common.Results;
using Russian.Post.Forms;
using Russian.Post.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Services.ClientMessages
{
    public interface IClientMessagesService
    {
        Task<PostResult> HandlePendingMessages();

        Task<PostResult> SendNewMessage(AddMessageForm form);

        Task<PostResult<IList<ClientMessage>>> AllDelivered();
    }
}
