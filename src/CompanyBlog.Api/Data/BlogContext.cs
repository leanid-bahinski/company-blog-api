using CompanyBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyBlog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Note>? Notes { get; set; } = null;
    }
}
