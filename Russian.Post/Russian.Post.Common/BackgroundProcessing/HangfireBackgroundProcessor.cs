﻿using Hangfire;
using Russian.Post.Common.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Russian.Post.Common.BackgroundProcessing
{
    internal sealed class HangfireBackgroundProcessor : IBackgroundProcessor
    {
        public void RecurrentJob<T>(Expression<Func<T, Task>> methodCall, Action<RecurringOptions> action = default)
        {
            var (recurringJobId, cronExpression) = Options(action);
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression);
        }

        public void RemoveRecurringJob(string jobId) => RecurringJob.RemoveIfExists(jobId);

        private static (string, Func<string>) Options(Action<RecurringOptions> action)
        {
            if (action == null)
                return ("Default-Not-Delivered-Job", Cron.Minutely);

            var options = new RecurringOptions();
            action?.Invoke(options);

            return (options.RecurringJobId, () => options.Cron);
        }
    }
}
