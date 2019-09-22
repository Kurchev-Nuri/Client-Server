using Hangfire;
using Russian.Post.Common.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Russian.Post.Common.BackgroundProcessing
{
    internal sealed class HangfireBackgroundProcessor : IBackgroundProcessor
    {
        public void RecurrentJob<T>(Expression<Func<T, Task>> methodCall, Action<RecurringOptions> action)
        {
            var (recurringJobId, cronExpression) = Options(action);
            RecurringJob.AddOrUpdate<T>(recurringJobId, methodCall, cronExpression);
        }

        public void RemoveRecurringJob(string jobId) => RecurringJob.RemoveIfExists(jobId);

        private static (string, Func<string>) Options(Action<RecurringOptions> action)
        {
            var options = new RecurringOptions();
            action?.Invoke(options);

            if (options == null)
                return ("Default-Not-Delivered-Job", Cron.Minutely);

            return (options.RecurringJobId, () => options.Cron);
        }
    }
}
