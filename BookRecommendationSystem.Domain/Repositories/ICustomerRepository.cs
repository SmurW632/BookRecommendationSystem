using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByUserIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
    }
}
