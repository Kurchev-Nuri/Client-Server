using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Russian.Post.Common.Results;

namespace Russian.Post.Server.Controllers.Base
{
    [ApiController]
    public class PostBaseController : ControllerBase
    {
        protected IActionResult PostResult(PostResult result) => HandleResult(result, StatusCodes.Status200OK);

        protected IActionResult PostResult<TResult>(PostResult<TResult> result) => HandleResult(result, StatusCodes.Status200OK);

        protected IActionResult PostResult<TResult>(TResult result) => HandleResult(new PostResult<TResult>(result), StatusCodes.Status200OK);

        protected IActionResult AlgoError(PostError error, int statusCode) => HandleResult(new PostResult { Error = error }, statusCode);

        protected IActionResult PostError<TError>(TError error, int statusCode)
            where TError : PostError
        {
            return HandleResult(new PostResult { Error = error }, statusCode);
        }

        private static IActionResult HandleResult<TResult>(TResult result, int statusCode)
            where TResult : PostResult
        {
            if (result.IsCorrect)
                return new JsonResult(result);

            return new JsonResult(result.Error) { StatusCode = statusCode };
        }
    }
}
