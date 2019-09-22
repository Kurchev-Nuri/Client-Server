using Russian.Post.Business.Logic.Specifications.Base;
using Russian.Post.Database.Models;

namespace Russian.Post.Business.Logic.Specifications.Client
{
    internal sealed class ClientPendingMessagesSpecification : BaseSpecification<PostClientMessage>
    {
        public ClientPendingMessagesSpecification()
            : base(message => !message.IsDeleted && !message.IsDelivered)
        {
        }
    }
}
