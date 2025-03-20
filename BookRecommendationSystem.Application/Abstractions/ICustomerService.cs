using BookRecommendationSystem.Application.DTOs;

namespace BookRecommendationSystem.Application.Abstractions
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(CustomerDto userDto);
        Task UpdateCustomerAsync(CustomerDto userDto);
        Task DeleteCustomerAsync(int id);
    }
}
