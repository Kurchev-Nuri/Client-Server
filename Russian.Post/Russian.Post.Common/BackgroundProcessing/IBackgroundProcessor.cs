using Russian.Post.Common.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Russian.Post.Common.BackgroundProcessing
{
    public interface IBackgroundProcessor
    {
        void RemoveRecurringJob(string jobId);

        void RecurrentJob<T>(Expression<Func<T, Task>> methodCall, Action<RecurringOptions> action);
    }
}
