using BookRecommendationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRecommendationSystem.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
