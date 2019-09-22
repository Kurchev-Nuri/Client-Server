using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Russian.Post.Business.Logic.Services.ServerMessages;
using Russian.Post.Forms;
using Russian.Post.Server.Controllers.Base;
using System.Threading.Tasks;

namespace Russian.Post.Server.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : PostBaseController
    {
        private readonly IServerMessagesService _messagesService;

        public MessageController(IServerMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpGet("start")]
        public IActionResult Get() => PostResult("Server started!");

        [HttpPost("send")]
        public async Task<IActionResult> PostMessage([FromBody] AddMessageForm form)
        {
            var result = await _messagesService.AddMessage(form);
            if (!result.IsCorrect)
                return PostError(result.Error, StatusCodes.Status400BadRequest);

            return PostResult(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListMessages()
        {
            var result = await _messagesService.AllMessages();
            return PostResult(result);
        }
    }
}
