using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Russian.Post.Common.Validation.Factories
{
    internal class ValidatorsFactory : IValidatorsFactory
    {
        private readonly IServiceProvider _provider;

        public ValidatorsFactory(IServiceProvider provider) => _provider = provider;

        public IValidator<TInstance> Resolve<TInstance>() => _provider.GetRequiredService<IValidator<TInstance>>();
    }
}
