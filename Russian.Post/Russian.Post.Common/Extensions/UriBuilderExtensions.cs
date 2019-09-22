using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Russian.Post.Common.Extensions
{
    public static class UriBuilderExtensions
    {
        public static UriBuilder AttachQuery<T>(this UriBuilder builder, T parametrs)
        {
            if (EqualityComparer<T>.Default.Equals(parametrs, default))
                return builder;

            var query = HttpUtility.ParseQueryString(builder.Query, Encoding.UTF8);
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetValue(parametrs) != null)
                    query[property.Name] = property.GetValue(parametrs).ToString();
            }

            builder.Query = query.ToString();
            return builder;
        }
    }
}
