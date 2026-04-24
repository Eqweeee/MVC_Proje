namespace MVC_Proje.Models
{
    using Microsoft.EntityFrameworkCore;
    using MVC_Proje.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
