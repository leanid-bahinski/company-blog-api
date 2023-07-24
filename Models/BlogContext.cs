using Microsoft.EntityFrameworkCore;

namespace CompanyBlog.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Note>? Notes { get; set; } = null;
    }
}
