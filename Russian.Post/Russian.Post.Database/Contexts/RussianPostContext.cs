using Microsoft.EntityFrameworkCore;
using Russian.Post.Database.Models;

namespace Russian.Post.Database.Contexts
{
    public class RussianPostContext : DbContext
    {
        public RussianPostContext(DbContextOptions<RussianPostContext> options)
            : base(options)
        { }

        public DbSet<PostClientMessage> PostClientMessages { get; set; }

        public DbSet<PostServerMessage> PostServerMessages { get; set; }
    }
}
