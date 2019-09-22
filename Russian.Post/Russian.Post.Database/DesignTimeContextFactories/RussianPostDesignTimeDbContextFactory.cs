using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Russian.Post.Database.DesignTimeContextFactories.BaseFactories;
using Russian.Post.Database.Contexts;
using Russian.Post.Consts;

namespace Russian.Post.Database.DesignTimeContextFactories
{
    public class RussianPostDesignTimeDbContextFactory : BaseDesignTimeDbContextFactory<RussianPostContext>
    {
        protected override RussianPostContext BuildContext(IConfigurationRoot configuration)
        {
            var builder = new DbContextOptionsBuilder<RussianPostContext>()
                .UseSqlServer(configuration.GetConnectionString(DatabaseConsts.PostMigrationConnectionString));

            return new RussianPostContext(builder.Options);
        }
    }
}
