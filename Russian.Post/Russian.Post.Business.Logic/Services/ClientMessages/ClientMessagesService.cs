using AutoMapper;
using Russian.Post.Business.Logic.Repositories.ClientMessages;
using Russian.Post.Business.Logic.Specifications.Client;
using Russian.Post.Common.Extensions;
using Russian.Post.Common.Results;
using Russian.Post.Common.Validation.FluentValidator;
using Russian.Post.Database.Models;
using Russian.Post.Forms;
using Russian.Post.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Services.ClientMessages
{
    internal sealed class ClientMessagesService : IClientMessagesService
    {
        private readonly IMapper _mapper;
        private readonly IFluentClientValidator _validator;

        public ClientMessagesService(IMapper mapper, IFluentClientValidator validator, IClientMessagesRepository repository)
        {
            _mapper = mapper;
            _validator = validator;
            MessagesRepository = repository;
        }

        public IClientMessagesRepository MessagesRepository { get; }

        public async Task<PostResult<ClientMessage>> AddMessage(AddMessageForm form)
        {
            var validate = _validator.Validate(form);
            if (!validate.IsValid)
                return validate.Errors.ConvertToAlgoError<ClientMessage>();

            var message = await MessagesRepository.AddAsync(new PostClientMessage
            {
                Message = form.Message
            });

            if (!message.IsCorrect)
                return message.ConvertErrorTo<ClientMessage>();

            return _mapper.Map<PostResult<ClientMessage>>(message.Result);
        }

        public Task<IList<ClientMessage>> AllPendingMessages() => MessagesRepository.AllAsync(new ClientPendingMessagesSpecification());

        public async Task<PostResult> MarkAsDelivered(int messageId)
        {
            var message = await MessagesRepository
                .FirstOrDefaultAsync(new ClientMessageSpecification(messageId), trackable: true);

            if (message == null)
                return PostResult.WithError(PostErrorCodes.EntityWasNotFound);

            message.IsDelivered = true;

            return await MessagesRepository.Update(message);
        }
    }
}
