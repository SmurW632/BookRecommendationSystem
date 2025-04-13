using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId);
        Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId);
        Task<IEnumerable<Book>> GetTopRatedBooksAsync(int count);
        Task<IEnumerable<Book>> GetRecommendedBooksAsync(int userId);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task RemoveAsync(Book book);

        // Методы для работы с рейтингами
        Task AddRatingAsync(int bookId, Rating rating);
        Task UpdateRatingAsync(Rating rating);
        Task RemoveRatingAsync(int bookId, int ratingId);

        // Методы для работы с рекомендациями
        Task AddRecommendationAsync(int bookId, Recommendation recommendation);
        Task UpdateRecommendationAsync(Recommendation recommendation);
        Task RemoveRecommendationAsync(int bookId, int recommendationId);

        // Для работы с жанрами
        Task AddGenreToBookAsync(int bookId, int genreId);
        Task RemoveGenreFromBookAsync(int bookId, int genreId);
    }
}
