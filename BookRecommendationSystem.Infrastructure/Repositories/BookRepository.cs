using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookRecommendationSystem.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        public readonly AppDbContext _context;

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
            var book = await _context.Books
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == bookId)
                ?? throw new Exception("Книга не найдена.");

            book.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task AddRecommendationAsync(int bookId, Recommendation recommendation)
        {
            var book = await _context.Books
                .Include(b => b.Recommendations)
                .FirstOrDefaultAsync(b => b.Id == bookId)
                ?? throw new Exception("Книга не найдена.");

            book.Recommendations.Add(recommendation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Ratings)
                .Include(b => b.Recommendations)
                .FirstOrDefaultAsync(b => b.Id == id)
                ?? throw new Exception("Книга не найдена");

            _context.Ratings.RemoveRange(book.Ratings);
            _context.Recommendations.RemoveRange(book.Recommendations);
            _context.Books.Remove(book);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRatingAsync(int bookId, int ratingId)
        {
            var book = await _context.Books
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == bookId)
                ?? throw new Exception("Книга не найдена.");

            var rating = book.Ratings.FirstOrDefault(b => b.Id == ratingId)
                ?? throw new Exception("Рейтинг не найден.");

            book.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecommendationAsync(int bookId, int recommendationId)
        {
            var book = await _context.Books
                .Include(b => b.Recommendations)
                .FirstOrDefaultAsync(b => b.Id == bookId)
                ?? throw new Exception("Книга не найдена.");

            var recommendation = book.Recommendations.FirstOrDefault(b => b.Id == recommendationId)
                ?? throw new Exception("Рекомендация не найдена.");

            book.Recommendations.Remove(recommendation);
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
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Ratings)
                .Include(b => b.Recommendations)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRatingAsync(int bookId, Rating rating)
        {
            var book = await _context.Books
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == bookId)
                ?? throw new Exception("Книга не найдена.");

            var existingRating = book.Ratings
                .FirstOrDefault(r => r.Id == rating.Id)
                ?? throw new Exception("Рейтинг не найден.");

            existingRating.Score = rating.Score;
            existingRating.Review = rating.Review;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecommendationAsync(int bookId, Recommendation recommendation)
        {
            var book = await _context.Books
                .Include(b => b.Recommendations)
                .FirstOrDefaultAsync(b => b.Id == bookId)
                ?? throw new Exception("Книга не найдена.");

            var existingRecommendation = book.Recommendations.FirstOrDefault(r => r.Id == recommendation.Id)
                ?? throw new Exception("Рекомендация не найдена.");

            existingRecommendation.Reason = recommendation.Reason;
            existingRecommendation.Score = recommendation.Score;

            await _context.SaveChangesAsync();
        }
    }
}
