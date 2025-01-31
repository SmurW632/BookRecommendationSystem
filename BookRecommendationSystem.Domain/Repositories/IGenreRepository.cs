using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Domain.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> GetByIdAsync(Guid id);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByNameAsync(string name);
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(Guid id);
    }
}
