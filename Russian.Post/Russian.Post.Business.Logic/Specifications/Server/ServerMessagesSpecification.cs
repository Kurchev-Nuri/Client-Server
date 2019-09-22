using Russian.Post.Business.Logic.Specifications.Base;
using Russian.Post.Database.Models;

namespace Russian.Post.Business.Logic.Specifications.Server
{
    internal sealed class ServerMessagesSpecification : BaseSpecification<PostServerMessage>
    {
        public ServerMessagesSpecification()
            : base(message => !message.IsDeleted)
        {
        }
    }
}
