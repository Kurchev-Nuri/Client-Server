using FluentValidation;

namespace Russian.Post.Forms.Validators
{
    internal sealed class AddMessageFormValidator : AbstractValidator<AddMessageForm>
    {
        public AddMessageFormValidator()
        {
            RuleFor(u => u.Message).NotEmpty()
                .WithMessage($"{nameof(AddMessageForm.Message)} has to be defined.");
        }
    }
}
