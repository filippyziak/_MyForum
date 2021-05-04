using Microsoft.EntityFrameworkCore;
using MyForum.Data.Configs;
using MyForum.Models.Domain.Auth;
using MyForum.Models.Domain.Post;

namespace MyForum.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new UserConfig().Configure(builder.Entity<User>());
            new PostConfig().Configure(builder.Entity<Post>());
        }
    }
}