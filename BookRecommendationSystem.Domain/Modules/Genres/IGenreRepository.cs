namespace BookRecommendationSystem.Domain.Modules.Genres
{
    public interface IGenreRepository
    {
        Task<Genre?> GetByIdAsync(int id);
        Task<IEnumerable<Genre?>> GetAllAsync();
        Task<Genre?> GetByNameAsync(string name);
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(Genre genre);
    }
}
