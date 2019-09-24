using Russian.Post.Common.Results;
using Russian.Post.Forms;
using Russian.Post.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Services.LocalClientMessages
{
    public interface ILocalClientMessagesService
    {
        Task<IList<ClientMessage>> AllPendingMessages();

        Task<PostResult> MarkAsDelivered(int messageId);

        Task<PostResult> IncrementAttempt(int messageId);

        Task<PostResult<ClientMessage>> AddMessage(AddMessageForm form);
    }
}
