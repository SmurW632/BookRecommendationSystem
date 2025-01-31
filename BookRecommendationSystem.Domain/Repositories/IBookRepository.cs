using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<IEnumerable<Book>> GetByAuthorIdAsync(Guid authorId);
        Task<IEnumerable<Book>> GetByGenreIdAsync(Guid genreId);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Guid id);

        // Методы для работы с рейтингами
        Task AddRatingAsync(Guid bookId, Rating rating);
        Task UpdateRatingAsync(Guid bookId, Rating rating);
        Task DeleteRatingAsync(Guid bookId, Guid ratingId);

        // Методы для работы с рекомендациями
        Task AddRecommendationAsync(Guid bookId, Recommendation recommendation);
        Task UpdateRecommendationAsync(Guid bookId, Recommendation recommendation);
        Task DeleteRecommendationAsync(Guid bookId, Guid recommendationId);
    }
}
