using Newtonsoft.Json;

namespace Russian.Post.Common.Results
{
    public class PostResult
    {
        public PostResult(PostError error = default) => Error = error;

        [JsonIgnore]
        public bool IsCorrect => Error == null || Error.Code == PostErrorCodes.Ok;

        public PostError Error { get; set; }

        public static PostResult Default => new PostResult();

        public static PostResult WithError(PostErrorCodes code)
        {
            return new PostResult(new PostError { Code = code });
        }

        public PostResult<TResult> ConvertErrorTo<TResult>() => new PostResult<TResult> { Error = Error };
    }

    public class PostResult<TResult> : PostResult
    {
        public PostResult(TResult result = default, PostError error = default)
            : base(error)
        {
            Result = result;
        }

        public TResult Result { get; set; }

        public static new PostResult<TResult> Default => new PostResult<TResult>();

        public static new PostResult<TResult> WithError(PostErrorCodes code)
        {
            return new PostResult<TResult>(error: new PostError { Code = code });
        }
    }
}
