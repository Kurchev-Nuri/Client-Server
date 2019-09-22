using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Russian.Post.Client.Options
{
    internal sealed class ContainerJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public ContainerJobActivator(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public override object ActivateJob(Type type) => _serviceProvider.GetRequiredService(type);
    }
}
