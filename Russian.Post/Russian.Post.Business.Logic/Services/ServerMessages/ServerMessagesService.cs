using Microsoft.AspNetCore.Http;
using Russian.Post.Business.Logic.Repositories.ServerMessages;
using Russian.Post.Business.Logic.Specifications.Server;
using Russian.Post.Common.Extensions;
using Russian.Post.Common.Results;
using Russian.Post.Common.Validation.FluentValidator;
using Russian.Post.Database.Models;
using Russian.Post.Forms;
using Russian.Post.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Russian.Post.Business.Logic.Services.ServerMessages
{
    internal sealed class ServerMessagesService : IServerMessagesService
    {
        private readonly IFluentClientValidator _validator;
        private readonly IHttpContextAccessor _contextAccessor;

        public ServerMessagesService(IHttpContextAccessor httpContext, IFluentClientValidator validator, IServerMessagesRepository repository)
        {
            _validator = validator;
            _contextAccessor = httpContext;
            MessagesRepository = repository;
        }

        public IServerMessagesRepository MessagesRepository { get; }

        public async Task<PostResult> AddMessage(AddMessageForm form)
        {
            var validate = _validator.Validate(form);
            if (!validate.IsValid)
                return validate.Errors.ConvertToAlgoError<PostResult>();

            var ipAddress = _contextAccessor.HttpContext.Request.Host.ToString();

            return await MessagesRepository.AddAsync(new PostServerMessage
            {
                IpAddress = ipAddress,
                Message = form.Message,
                CreatedAt = DateTimeOffset.UtcNow
            });
        }

        public Task<IList<ServerMessage>> AllMessages() => MessagesRepository.AllAsync(new ServerMessagesSpecification());
    }
}
