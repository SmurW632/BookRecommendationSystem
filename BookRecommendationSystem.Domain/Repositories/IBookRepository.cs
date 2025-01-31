using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId);
        Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);

        // Методы для работы с рейтингами
        Task AddRatingAsync(int bookId, Rating rating);
        Task UpdateRatingAsync(int bookId, Rating rating);
        Task DeleteRatingAsync(int bookId, int ratingId);

        // Методы для работы с рекомендациями
        Task AddRecommendationAsync(int bookId, Recommendation recommendation);
        Task UpdateRecommendationAsync(int bookId, Recommendation recommendation);
        Task DeleteRecommendationAsync(int bookId, int recommendationId);
    }
}
