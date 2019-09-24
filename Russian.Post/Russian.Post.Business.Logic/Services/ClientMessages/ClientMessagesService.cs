using Microsoft.Extensions.Options;
using Russian.Post.Business.Logic.Services.LocalClientMessages;
using Russian.Post.Common.Extensions;
using Russian.Post.Common.HttpRequestService;
using Russian.Post.Common.Options;
using Russian.Post.Common.Results;
using Russian.Post.Common.Validation.FluentValidator;
using Russian.Post.Forms;
using Russian.Post.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Services.ClientMessages
{
    internal sealed class ClientMessagesService : IClientMessagesService
    {
        private readonly IRequestService _requestService;
        private readonly IFluentClientValidator _validator;
        private readonly ILocalClientMessagesService _localMessages;
        private readonly IOptionsMonitor<ApiEndpointOptions> _options;

        public ClientMessagesService(IRequestService requestService,
                                     IFluentClientValidator validator,
                                     ILocalClientMessagesService messages,
                                     IOptionsMonitor<ApiEndpointOptions> options)
        {
            _options = options;
            _validator = validator;
            _localMessages = messages;
            _requestService = requestService;
        }

        private ApiEndpointOptions Options => _validator.ValidateAndThrow(_options.CurrentValue);

        public Task<PostResult<IList<ServerMessage>>> AllDelivered()
        {
            return _requestService.MakeGetRequest<IList<ServerMessage>>(Options.DeliveredUrl);
        }

        public async Task<PostResult> SendNewMessage(AddMessageForm form)
        {
            var validate = _validator.Validate(form);
            if (!validate.IsValid)
                return validate.Errors.ConvertToAlgoError();

            var message = await _localMessages.AddMessage(form);
            if (!message.IsCorrect)
                return message;

            var request = await _requestService.MakePostRequest<AddMessageForm, ServerMessage>(Options.SendUrl, form);
            if (!request.IsCorrect)
                return request;

            if (request.IsCorrect)
                await _localMessages.MarkAsDelivered(message.Result.Id);
            else
                await _localMessages.IncrementAttempt(message.Result.Id);

            return PostResult.Default;
        }

        public async Task<PostResult> HandlePendingMessages()
        {
            var messages = await _localMessages.AllPendingMessages();
            if (!messages.Any())
                return PostResult.Default;

            foreach (var value in messages)
            {
                var request = await _requestService.MakePostRequest<AddMessageForm, ServerMessage>(Options.SendUrl, new AddMessageForm
                {
                    Message = value.Message
                });

                if (request.IsCorrect)
                    await _localMessages.MarkAsDelivered(value.Id);
                else
                    await _localMessages.IncrementAttempt(value.Id);
            }

            return PostResult.Default;
        }
    }
}
