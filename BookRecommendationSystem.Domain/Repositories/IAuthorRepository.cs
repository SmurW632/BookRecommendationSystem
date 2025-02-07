using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author?> GetByIdAsync(int id);
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByNameAsync(string name);
        Task AddAsync(Author authorDto);
        Task UpdateAsync(Author authorDto);
        Task DeleteAsync(Author author);
    }
}
