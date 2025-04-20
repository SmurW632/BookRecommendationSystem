using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Modules.Authors;
using BookRecommendationSystem.Domain.Modules.Books;
using BookRecommendationSystem.Domain.Modules.Customers;
using BookRecommendationSystem.Domain.Modules.Genres;
using BookRecommendationSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookRecommendationSystem.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string? dbConnectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(dbConnectionString));

            // Регистрация репозиториев
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
