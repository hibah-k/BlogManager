
using Microsoft.EntityFrameworkCore;
using BlogPostManager.Models;

namespace BlogPostManager.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
    }
}
