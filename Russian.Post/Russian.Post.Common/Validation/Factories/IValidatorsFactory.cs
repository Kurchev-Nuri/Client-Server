using FluentValidation;

namespace Russian.Post.Common.Validation.Factories
{
    internal interface IValidatorsFactory
    {
        IValidator<TInstance> Resolve<TInstance>();
    }
}
