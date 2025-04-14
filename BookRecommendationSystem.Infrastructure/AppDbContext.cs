using BookRecommendationSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookRecommendationSystem.Infrastructure
{
    public class AppDbContext : IdentityDbContext<UserEntity, IdentityRoleEntity, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<BookGenre> BookGenres { get; set; } = null!;
        public DbSet<UserLibrary> UserLibraries { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<Recommendation> Recommendations { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Identity таблицы
            modelBuilder.Entity<UserEntity>(b =>
            {
                b.ToTable("AspNetUsers");
                b.HasOne(u => u.Customer)
                 .WithOne(c => c.User)
                 .HasForeignKey<Customer>(c => c.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<IdentityRoleEntity>(b =>
            {
                b.ToTable("AspNetRoles");
            });

            // 2. Book ↔ Author (1:N)
            modelBuilder.Entity<Book>(b =>
            {
                b.HasOne(b => b.Author)
                 .WithMany(a => a.Books)
                 .HasForeignKey(b => b.AuthorId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // 3. Book ↔ Genre (M:N через BookGenre)
            modelBuilder.Entity<BookGenre>(b =>
            {
                b.HasKey(bg => new { bg.BookId, bg.GenreId });

                b.HasOne(bg => bg.Book)
                 .WithMany(b => b.Genres)
                 .HasForeignKey(bg => bg.BookId);

                b.HasOne(bg => bg.Genre)
                 .WithMany(g => g.BookGenres)
                 .HasForeignKey(bg => bg.GenreId);
            });

            // 4. Customer ↔ UserLibrary (1:N)
            modelBuilder.Entity<UserLibrary>(b =>
            {
                b.HasOne(ul => ul.Customer)
                 .WithMany(c => c.Libraries)
                 .HasForeignKey(ul => ul.CustomerId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(ul => ul.Book)
                 .WithMany(b => b.UserLibraries)
                 .HasForeignKey(ul => ul.BookId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(ul => new { ul.CustomerId, ul.BookId }).IsUnique();
            });

            // 5. Customer ↔ Rating (1:N)
            modelBuilder.Entity<Rating>(b =>
            {
                b.HasOne(r => r.Customer)
                 .WithMany(c => c.Ratings)
                 .HasForeignKey(r => r.CustomerId);

                b.HasOne(r => r.Book)
                 .WithMany(b => b.Ratings)
                 .HasForeignKey(r => r.BookId);

                b.HasIndex(r => new { r.CustomerId, r.BookId }).IsUnique();
            });

            // 6. Customer ↔ Recommendation (1:N)
            modelBuilder.Entity<Recommendation>(b =>
            {
                b.HasOne(r => r.Customer)
                 .WithMany(c => c.Recommendations)
                 .HasForeignKey(r => r.CustomerId)
                 .OnDelete(DeleteBehavior.SetNull);

                b.HasOne(r => r.Book)
                 .WithMany(b => b.Recommendations)
                 .HasForeignKey(r => r.BookId);
            });

            // Валидации
            modelBuilder.Entity<Book>(b =>
            {
                b.Property(x => x.PublishedYear)
                 .HasAnnotation("Minimum", 1800)
                 .HasAnnotation("Maximum", 2100);
            });
        }
    }
}
