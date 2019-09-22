using FluentValidation.Results;

namespace Russian.Post.Common.Validation.FluentValidator
{
    public interface IFluentClientValidator
    {
        ValidationResult Validate<TInstance>(TInstance instance)
            where TInstance : class, new();

        TInstance ValidateAndThrow<TInstance>(TInstance instance)
           where TInstance : class, new();
    }
}
