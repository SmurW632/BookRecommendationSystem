using System.Net;
using System.Net.Http.Json;
using BookRecommendationSystem.Application.DTOs.Authentication;
using Microsoft.AspNetCore.Identity;
using BookRecommendationSystem.Domain.Entities;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    private readonly UserManager<UserEntity> _userManager;

    public AccountControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();

        // Получаем UserManager
        var scope = factory.Services.CreateScope();
        _userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
    }

    [Fact]
    public async Task Login_ReturnsToken_ForValidCredentials()
    {
        // Arrange
        const string testEmail = "test@example.com";
        const string testPassword = "Test123!";

        // Создаем тестового пользователя
        var user = new UserEntity { UserName = testEmail, Email = testEmail };
        await _userManager.CreateAsync(user, testPassword);

        var loginDto = new UserLoginDto
        {
            Email = testEmail,
            Password = testPassword
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/account/login", loginDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var tokenResponse = await response.Content.ReadFromJsonAsync<UserResponse>();
        tokenResponse.Should().NotBeNull();
        tokenResponse.Token.Should().NotBeNullOrEmpty();
    }

    public async Task InitializeAsync()
    {
        // Очищаем базу перед каждым тестом
        await CleanupDatabase();
    }

    public async Task DisposeAsync()
    {
        // Очищаем базу после каждого теста
        await CleanupDatabase();
    }

    private async Task CleanupDatabase()
    {
        // Удаляем всех пользователей
        foreach (var user in _userManager.Users.ToList())
        {
            await _userManager.DeleteAsync(user);
        }
    }
}