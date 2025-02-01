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
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<Recommendation> Recommendations { get; set;} = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка сущности Author
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id); // Указываем первичный ключ
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100); // Ограничение на длину имени
            });

            // Настройка сущности Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id); // Первичный ключ
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200); // Ограничение на длину названия
                entity.HasOne(b => b.Author) // Связь с Author
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorId);
                entity.HasOne(b => b.Genre) // Связь с Genre
                      .WithMany(g => g.Books)
                      .HasForeignKey(b => b.GenreId);
            });

            // Настройка сущности Genre
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.Id); // Первичный ключ
                entity.Property(g => g.Name).IsRequired().HasMaxLength(100); // Ограничение на длину названия жанра
            });

            // Настройка сущности User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id); // Первичный ключ
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50); // Ограничение на длину имени пользователя
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100); // Ограничение на длину email
            });

            // Настройка сущности Rating
            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(r => r.Id); // Первичный ключ
                entity.HasOne(r => r.Book) // Связь с Book
                      .WithMany(b => b.Ratings)
                      .HasForeignKey(r => r.BookId);
            });

            // Настройка сущности Recommendation
            modelBuilder.Entity<Recommendation>(entity =>
            {
                entity.HasKey(r => r.Id); // Первичный ключ
                entity.HasOne(r => r.Book) // Связь с Book
                      .WithMany(b => b.Recommendations)
                      .HasForeignKey(r => r.BookId);
                entity.HasOne(r => r.User) // Связь с User
                      .WithMany(u => u.Recommendations)
                      .HasForeignKey(r => r.UserId);
            });
        }
    }
}
