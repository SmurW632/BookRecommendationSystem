using BookRecommendationSystem.Application.Services;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Exceptions;
using BookRecommendationSystem.Domain.Repositories;
using Castle.Core.Resource;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepo;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _mockRepo = new Mock<ICustomerRepository>();
            _service = new CustomerService(_mockRepo.Object, null);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsCustomer_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer { Id = 1, Nickname = "testNickname" };
            _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(customer);

            // Act
            var result = await _service.GetCustomerByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be("test@example.com");
        }

        [Fact]
        public async Task GetCustomerById_ThrowsNotFoundException_WhenCustomerNotExists()
        {
            // Arrange
            _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Customer)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetCustomerByIdAsync(1));
        }
    }
}
