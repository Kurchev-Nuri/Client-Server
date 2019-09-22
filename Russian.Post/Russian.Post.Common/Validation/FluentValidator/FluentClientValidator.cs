using FluentValidation;
using FluentValidation.Results;
using Russian.Post.Common.Validation.Factories;

namespace Russian.Post.Common.Validation.FluentValidator
{
    internal sealed class FluentClientValidator : IFluentClientValidator
    {
        private readonly IValidatorsFactory _factory;

        public FluentClientValidator(IValidatorsFactory factory) => _factory = factory;

        public ValidationResult Validate<TInstance>(TInstance instance)
            where TInstance : class, new()
        {
            return _factory.Resolve<TInstance>()
                .Validate(instance);
        }

        public TInstance ValidateAndThrow<TInstance>(TInstance instance)
            where TInstance : class, new()
        {
            _factory.Resolve<TInstance>()
               .ValidateAndThrow(instance);

            return instance;
        }
    }
}
