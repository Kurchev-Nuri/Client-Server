using FluentValidation.Results;
using Russian.Post.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Russian.Post.Common.Extensions
{
    public static class ValidationFailureExtension
    {
        public static PostResult ConvertToAlgoError(this IList<ValidationFailure> failures) => new PostResult
        {
            Error = Error(failures)
        };

        public static PostResult<TResult> ConvertToAlgoError<TResult>(this IList<ValidationFailure> failures) => new PostResult<TResult>
        {
            Error = Error(failures)
        };

        private static PostError Error(IList<ValidationFailure> failures)
        {
            return new PostError
            {
                Code = PostErrorCodes.InvalidInput,
                Message = string.Join(Environment.NewLine, failures.Select(u => u.ErrorMessage))
            };
        }
    }
}
