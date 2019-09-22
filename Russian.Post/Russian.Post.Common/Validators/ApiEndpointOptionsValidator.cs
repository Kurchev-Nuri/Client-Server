using FluentValidation;
using Russian.Post.Common.Options;

namespace Russian.Post.Common.Validators
{
    internal sealed class ApiEndpointOptionsValidator : AbstractValidator<ApiEndpointOptions>
    {
        public ApiEndpointOptionsValidator()
        {
            RuleFor(u => u.SendUrl).NotEmpty()
                .WithMessage($"{nameof(ApiEndpointOptions.SendUrl)} has to be defined.");
        }
    }
}
