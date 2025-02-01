using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Repositories;
using BookRecommendationSystem.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookRecommendationSystem.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private const string BOOK_NOT_FOUND = "Книга не найдена.";
        private const string RATING_NOT_FOUND = "Рейтинг не найден.";
        private const string RECOMMENDATION_NOT_FOUND = "Рекомендация не найдена.";

        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task AddRatingAsync(int bookId, Rating rating)
        {
            var book = await GetByIdAsync(bookId);

            book.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task AddRecommendationAsync(int bookId, Recommendation recommendation)
        {
            var book = await GetByIdAsync(bookId);

            book.Recommendations.Add(recommendation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await GetByIdAsync(id);

            _context.Ratings.RemoveRange(book.Ratings);
            _context.Recommendations.RemoveRange(book.Recommendations);
            _context.Books.Remove(book);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRatingAsync(int bookId, int ratingId)
        {
            var book = await GetByIdAsync(bookId);

            var rating = book.Ratings.FirstOrDefault(b => b.Id == ratingId);
            Guard.AgainstNull(rating, RATING_NOT_FOUND);

            book.Ratings.Remove(rating!);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecommendationAsync(int bookId, int recommendationId)
        {
            var book = await GetByIdAsync(bookId);

            var recommendation = book.Recommendations.FirstOrDefault(b => b.Id == recommendationId);
            Guard.AgainstNull(recommendation, RECOMMENDATION_NOT_FOUND);

            book.Recommendations.Remove(recommendation!);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Ratings)
                .Include(b => b.Recommendations)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Ratings)
                .Include(b => b.Recommendations)
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Ratings)
                .Include(b => b.Recommendations)
                .Where(b => b.GenreId == genreId)
                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Ratings)
                .Include(b => b.Recommendations)
                .FirstOrDefaultAsync(b => b.Id == id);
            Guard.AgainstNull(book, BOOK_NOT_FOUND);

            return book!;
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRatingAsync(int bookId, Rating rating)
        {
            var book = await GetByIdAsync(bookId);

            var existingRating = book!.Ratings
                .FirstOrDefault(r => r.Id == rating.Id);
            Guard.AgainstNull(existingRating, RATING_NOT_FOUND);

            existingRating!.Score = rating.Score;
            existingRating.Review = rating.Review;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecommendationAsync(int bookId, Recommendation recommendation)
        {
            var book = await GetByIdAsync(bookId);

            var existingRecommendation = book!.Recommendations.FirstOrDefault(r => r.Id == recommendation.Id);
            Guard.AgainstNull(existingRecommendation, RECOMMENDATION_NOT_FOUND);

            existingRecommendation!.Reason = recommendation.Reason;
            existingRecommendation.Score = recommendation.Score;

            await _context.SaveChangesAsync();
        }
    }
}
