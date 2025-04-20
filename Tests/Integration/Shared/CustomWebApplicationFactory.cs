using BookRecommendationSystem.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Удаляем существующие регистрации баз данных
            RemoveService<DbContextOptions<AppDbContext>>(services);
            RemoveService<DbContextOptions<IdentityDbContext>>(services);

            // Добавляем InMemory Database
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseInMemoryDatabase("IdentityDb"));

            // Инициализируем базы данных
            using var scope = services.BuildServiceProvider().CreateScope();
            scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
            scope.ServiceProvider.GetRequiredService<IdentityDbContext>().Database.EnsureCreated();
        });
    }

    private static void RemoveService<T>(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
        if (descriptor != null) services.Remove(descriptor);
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Важно: создаем тестовый хост перед основным
        var testHost = base.CreateHost(builder);

        using var scope = testHost.Services.CreateScope();
        var services = scope.ServiceProvider;

        // Инициализация баз данных
        services.GetRequiredService<AppDbContext>().Database.EnsureCreated();
        services.GetRequiredService<IdentityDbContext>().Database.EnsureCreated();

        return testHost;
    }
}