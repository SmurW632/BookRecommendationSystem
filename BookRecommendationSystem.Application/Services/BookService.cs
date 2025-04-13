using AutoMapper;
using BookRecommendationSystem.Application.Abstractions;
using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Application.Helpers;
using BookRecommendationSystem.Domain.Aggregates;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Exceptions;
using BookRecommendationSystem.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace BookRecommendationSystem.Application.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ICustomerRepository _customerRepository;
        private IMapper _mapper;
        private readonly ILogger _logger;

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository,
            ICustomerRepository customerRepository,
            IMapper mapper,
            ILogger<BookService> logger)
        {
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddBookAsync(BookDto bookDto)
        {
            using var _ = _logger.BeginScope("Adding new book: {Title}", bookDto.Title);

            try
            {
                var book = _mapper.Map<Book>(bookDto);
                Guard.AgainstNull(book, nameof(book));

                var bookAggregate = new BookAggregate(
                    book,
                    _authorRepository,
                    _genreRepository,
                    _bookRepository,
                    _customerRepository);

                await bookAggregate.AddBookAsync(
                    bookDto.Title,
                    bookDto.Description,
                    bookDto.PublishedYear,
                    bookDto.CoverImageUrl,
                    bookDto.Author.Name,
                    bookDto.Author.Biography,
                    bookDto.Genres.Select(g => g.Name).ToList());

                await _bookRepository.AddAsync(book);
                _logger.LogInformation("Book {BookId} added successfully", book.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding book");
                throw;
            }
        }

        public async Task AddRatingAsync(int bookId, RatingDto ratingDto)
        {
            var book = await _bookRepository.GetByIdWithDetailsAsync(bookId);
            Guard.AgainstNull(book, nameof(book));

            var rating = _mapper.Map<Rating>(ratingDto);
            rating.CreatedAt = DateTime.UtcNow;

            await _bookRepository.AddRatingAsync(bookId, rating);
            _logger.LogInformation("Rating added to book {BookId} by user {UserId}",
                bookId, ratingDto.CustomerId);
        }

        public async Task AddRecommendationAsync(int bookId, RecommendationDto recommendationDto)
        {
            var book = await _bookRepository.GetByIdWithDetailsAsync(bookId);
            Guard.AgainstNull(book, nameof(book));

            var recommendation = _mapper.Map<Recommendation>(recommendationDto);
            recommendation.CreatedAt = DateTime.UtcNow;
            recommendation.ExpiresAt = DateTime.UtcNow.AddMonths(1);
            recommendation.IsActive = true;

            book.Recommendations.Add(recommendation);
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            Guard.AgainstNull(book, nameof(book));

            await _bookRepository.RemoveAsync(book);
            _logger.LogInformation("Book {BookId} deleted", id);
        }

        public async Task DeleteRatingAsync(int bookId, int ratingId)
        {
            await _bookRepository.RemoveRatingAsync(bookId, ratingId);
            _logger.LogInformation("Rating {RatingId} removed from book {BookId}",
                ratingId, bookId);
        }

        public async Task DeleteRecommendationAsync(int bookId, int recommendationId)
        {
            var book = await _bookRepository.GetByIdWithDetailsAsync(bookId);
            Guard.AgainstNull(book, nameof(book));

            var recommendation = book.Recommendations.FirstOrDefault(r => r.Id == recommendationId);
            Guard.AgainstNull(recommendation, nameof(recommendation));

            book.Recommendations.Remove(recommendation);
            await _bookRepository.UpdateAsync(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdWithDetailsAsync(id);
            Guard.AgainstNull(book, nameof(book));

            return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> GetByAuthorIdAsync(int authorId)
        {
            var books = await _bookRepository.GetByAuthorIdAsync(authorId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetByGenreIdAsync(int genreId)
        {
            var books = await _bookRepository.GetByGenreIdAsync(genreId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetTopRatedBooksAsync(int count)
        {
            var books = await _bookRepository.GetTopRatedBooksAsync(count);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetRecommendedBooksAsync(int userId)
        {
            var customer = await _customerRepository.GetByIdAsync(userId);
            Guard.AgainstNull(customer, nameof(customer));

            var books = await _bookRepository.GetRecommendedBooksAsync(userId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var existingBook = await _bookRepository.GetByIdWithDetailsAsync(bookDto.Id);
            Guard.AgainstNull(existingBook, nameof(existingBook));

            _mapper.Map(bookDto, existingBook);

            // Обновление жанров
            if (bookDto.Genres != null)
            {
                existingBook.Genres.Clear();
                foreach (var genreDto in bookDto.Genres)
                {
                    var genre = await _genreRepository.GetByNameAsync(genreDto.Name);
                    if (genre != null)
                    {
                        existingBook.Genres.Add(new BookGenre { GenreId = genre.Id });
                    }
                }
            }

            await _bookRepository.UpdateAsync(existingBook);
            _logger.LogInformation("Book {BookId} updated", bookDto.Id);
        }

        public async Task UpdateRatingAsync(int bookId, RatingDto ratingDto)
        {
            var rating = _mapper.Map<Rating>(ratingDto);
            rating.UpdatedAt = DateTime.UtcNow;

            await _bookRepository.UpdateRatingAsync(rating);
            _logger.LogInformation("Rating {RatingId} updated", ratingDto.Id);
        }

        public async Task UpdateRecommendationAsync(int bookId, RecommendationDto recommendationDto)
        {
            var book = await _bookRepository.GetByIdWithDetailsAsync(bookId);
            Guard.AgainstNull(book, nameof(book));

            var recommendation = book.Recommendations.FirstOrDefault(r => r.Id == recommendationDto.Id);
            Guard.AgainstNull(recommendation, nameof(recommendation));

            _mapper.Map(recommendationDto, recommendation);
            await _bookRepository.UpdateAsync(book);
        }
    }
}
