using BookRecommendationSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookRecommendationSystem.WebApi.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }
    }
}
