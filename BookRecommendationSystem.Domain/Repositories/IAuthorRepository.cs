using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(Guid id);
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByNameAsync(string name);
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(Guid id);
    }
}
