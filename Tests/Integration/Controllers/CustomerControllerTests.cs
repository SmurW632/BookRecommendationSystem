using System.Net;
using System.Net.Http.Json;
using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Application.DTOs.Authentication;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class CustomerControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<UserEntity> _userManager;
    private string _authToken;
    private int _testCustomerId;

    public CustomerControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        _userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
    }

    /* Инициализация и очистка */
    public async Task InitializeAsync()
    {
        // Создаем тестового пользователя и получаем токен
        _authToken = await RegisterAndLoginTestUser();
        _testCustomerId = await CreateTestCustomer("InitialCustomer");

        // Устанавливаем токен для авторизованных запросов
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
    }

    public async Task DisposeAsync() => await CleanupDatabase();

    private async Task CleanupDatabase()
    {
        _dbContext.Customers.RemoveRange(_dbContext.Customers);
        await _dbContext.SaveChangesAsync();

        foreach (var user in _userManager.Users)
            await _userManager.DeleteAsync(user);
    }

    /* Вспомогательные методы */
    private async Task<string> RegisterAndLoginTestUser()
    {
        // Регистрация
        var registerDto = new UserRegisterDto
        {
            Username = "testuser",
            Email = "testuser@example.com",
            Phone = "+1234567890",
            Password = "Test123!",
            ConfirmPassword = "Test123!"
        };

        await _client.PostAsJsonAsync("/api/account/register", registerDto);

        // Логин
        var loginDto = new UserLoginDto
        {
            Email = "testuser@example.com",
            Password = "Test123!"
        };

        var loginResponse = await _client.PostAsJsonAsync("/api/account/login", loginDto);
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<UserResponse>();
        return loginResult.Token;
    }

    private async Task<int> CreateTestCustomer(string nickname)
    {
        var customer = new Customer
        {
            Nickname = nickname,
            FirstName = "Test",
            LastName = "User",
            UserId = (await _userManager.FindByEmailAsync("testuser@example.com")).Id
        };

        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();
        return customer.Id;
    }

    /* ТЕСТЫ */

    [Fact]
    public async Task GetAllCustomers_ReturnsOk_WithCustomers()
    {
        // Act
        var response = await _client.GetAsync("/api/customer");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var customers = await response.Content.ReadFromJsonAsync<List<CustomerDto>>();
        customers.Should().NotBeEmpty();
        customers.Should().Contain(c => c.Nickname == "InitialCustomer");
    }

    [Fact]
    public async Task GetCustomerById_ReturnsCustomer_WhenExists()
    {
        // Act
        var response = await _client.GetAsync($"/api/customer/{_testCustomerId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();
        customer.Should().NotBeNull();
        customer.Id.Should().Be(_testCustomerId);
        customer.Nickname.Should().Be("InitialCustomer");
    }

    [Fact]
    public async Task AddCustomer_ReturnsCreated_WithValidData()
    {
        // Arrange
        var newCustomer = new CustomerDto
        {
            Nickname = "NewCustomer",
            FirstName = "New",
            LastName = "User"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/customer", newCustomer);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdCustomer = await response.Content.ReadFromJsonAsync<CustomerDto>();
        createdCustomer.Should().NotBeNull();
        createdCustomer.Nickname.Should().Be("NewCustomer");

        // Проверяем в БД
        var dbCustomer = await _dbContext.Customers.FindAsync(createdCustomer.Id);
        dbCustomer.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateCustomer_ReturnsNoContent_WhenValidData()
    {
        // Arrange
        var updatedCustomer = new CustomerDto
        {
            Id = _testCustomerId,
            Nickname = "UpdatedCustomer",
            FirstName = "Updated",
            LastName = "User"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/customer/{_testCustomerId}", updatedCustomer);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Проверяем обновление в БД
        var dbCustomer = await _dbContext.Customers.FindAsync(_testCustomerId);
        dbCustomer.Nickname.Should().Be("UpdatedCustomer");
    }

    [Fact]
    public async Task DeleteCustomer_ReturnsNoContent_WhenExists()
    {
        // Act
        var response = await _client.DeleteAsync($"/api/customer/{_testCustomerId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Проверяем удаление из БД
        var dbCustomer = await _dbContext.Customers.FindAsync(_testCustomerId);
        dbCustomer.Should().BeNull();
    }

    /* Дополнительные тесты для проверки безопасности */
    [Fact]
    public async Task ProtectedEndpoints_RequireAuthorization()
    {
        // Arrange - новый клиент без токена
        var unauthorizedClient = _factory.CreateClient();

        // Act + Assert
        var getResponse = await unauthorizedClient.GetAsync("/api/customer");
        getResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var postResponse = await unauthorizedClient.PostAsJsonAsync("/api/customer", new CustomerDto());
        postResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}