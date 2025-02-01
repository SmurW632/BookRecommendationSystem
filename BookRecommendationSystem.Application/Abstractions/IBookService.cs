using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Application.Abstractions
{
    public interface IBookService
    {
        Task<BookDto> GetBookByIdAsync(int id);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<IEnumerable<BookDto>> GetByAuthorIdAsync(int authorId);
        Task<IEnumerable<BookDto>> GetByGenreIdAsync(int genreId);
        Task AddBookAsync(BookDto book);
        Task UpdateBookAsync(BookDto book);
        Task DeleteBookAsync(int id);

        // Методы для работы с рейтингами
        Task AddRatingAsync(int bookId, RatingDto rating);
        Task UpdateRatingAsync(int bookId, RatingDto rating);
        Task DeleteRatingAsync(int bookId, int ratingId);

        // Методы для работы с рекомендациями
        Task AddRecommendationAsync(int bookId, RecommendationDto recommendation);
        Task UpdateRecommendationAsync(int bookId, RecommendationDto recommendation);
        Task DeleteRecommendationAsync(int bookId, int recommendationId);
    }
}
