using BookRecommendationSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class TestAuthHelper
{
    public static async Task CreateTestUser(
        CustomWebApplicationFactory factory,
        string email,
        string password)
    {
        using var scope = factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        var user = new UserEntity { UserName = email, Email = email };
        await userManager.CreateAsync(user, password);
    }

    public static async Task CleanupTestUsers(CustomWebApplicationFactory factory)
    {
        using var scope = factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        foreach (var user in userManager.Users.ToList())
        {
            await userManager.DeleteAsync(user);
        }
    }
}