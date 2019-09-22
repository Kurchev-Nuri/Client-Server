using Russian.Post.Common.Results;
using Russian.Post.Forms;
using Russian.Post.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Services.ServerMessages
{
    public interface IServerMessagesService
    {
        Task<IList<ServerMessage>> AllMessages();

        Task<PostResult> AddMessage(AddMessageForm form);
    }
}
