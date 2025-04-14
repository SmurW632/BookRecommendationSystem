using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookRecommendationSystem.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genres)
                    .ThenInclude(bg => bg.Genre)
                .Include(b => b.Ratings)
                .Include(b => b.Recommendations)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId)
        {
            return await _context.Books
                .Where(b => b.AuthorId == authorId)
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId)
        {
            return await _context.Books
                .Where(b => b.Genres.Any(g => g.GenreId == genreId))
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetTopRatedBooksAsync(int count)
        {
            return await _context.Books
                .Select(b => new
                {
                    Book = b,
                    AvgRating = b.Ratings.Average(r => r.Score)
                })
                .OrderByDescending(x => x.AvgRating)
                .Take(count)
                .Select(x => x.Book)
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetRecommendedBooksAsync(int userId)
        {
            return await _context.Recommendations
                .Where(r => r.Customer.UserId == userId && r.IsActive)
                .OrderByDescending(r => r.Score)
                .Select(r => r.Book)
                .Include(b => b.Author)
                .Distinct()
                .ToListAsync();
        }

        public async Task AddRatingAsync(int bookId, Rating rating)
        {
            var book = await _context.Books
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book != null)
            {
                book.Ratings.Add(rating);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveRatingAsync(int bookId, int ratingId)
        {
            var rating = await _context.Ratings
                .FirstOrDefaultAsync(r => r.Id == ratingId && r.BookId == bookId);

            if (rating != null)
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRatingAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task AddGenreToBookAsync(int bookId, int genreId)
        {
            var bookGenre = new BookGenre { BookId = bookId, GenreId = genreId };
            _context.BookGenres.Add(bookGenre);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveGenreFromBookAsync(int bookId, int genreId)
        {
            var bookGenre = await _context.BookGenres
                .FirstOrDefaultAsync(bg => bg.BookId == bookId && bg.GenreId == genreId);

            if (bookGenre != null)
            {
                _context.BookGenres.Remove(bookGenre);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id== id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToArrayAsync();
        }


        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Book book)
        {
            var removebook = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            if (removebook != null)
            {
                _context.Books.Remove(removebook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddRecommendationAsync(int bookId, Recommendation recommendation)
        {
            var book = await _context.Books
                .Include(b => b.Recommendations)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book != null)
            {
                book.Recommendations.Add(recommendation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRecommendationAsync(Recommendation recommendation)
        {
            _context.Recommendations.Update(recommendation);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRecommendationAsync(int bookId, int recommendationId)
        {
            var recommendation = await _context.Recommendations
                .FirstOrDefaultAsync(r => r.Id == recommendationId && r.BookId == bookId);

            if (recommendation != null)
            {
                _context.Recommendations.Remove(recommendation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
