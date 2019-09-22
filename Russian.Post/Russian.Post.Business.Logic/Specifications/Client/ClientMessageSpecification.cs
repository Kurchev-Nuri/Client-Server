using Russian.Post.Business.Logic.Specifications.Base;
using Russian.Post.Database.Models;

namespace Russian.Post.Business.Logic.Specifications.Client
{
    internal sealed class ClientMessageSpecification : BaseSpecification<PostClientMessage>
    {
        public ClientMessageSpecification(int messageId)
            : base(message => message.Id == messageId && !message.IsDeleted)
        {
        }
    }
}
