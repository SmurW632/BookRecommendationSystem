using AutoMapper;
using BookRecommendationSystem.Application.Abstractions;
using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Application.Helpers;
using BookRecommendationSystem.Domain.Aggregates;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.ExceptionMessageConsts;
using BookRecommendationSystem.Domain.Repositories;

namespace BookRecommendationSystem.Application.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private IMapper _mapper;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task AddBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            var bookAggregate = new BookAggregate(book, _authorRepository, _genreRepository);
            Guard.AgainstNull(bookAggregate, ExMesConsts.BOOK_NOT_FOUND);

            await bookAggregate.AddBookAsync(
                bookDto.Title,
                bookDto.Description,
                bookDto.PublishedYear,
                bookDto.CoverImageUrl,
                bookDto.Author.Name,
                bookDto.Author.Biography,
                bookDto.Genre.Name
                );

            await _bookRepository.AddAsync(book);
        }

        public async Task AddRatingAsync(int bookId, RatingDto ratingDto)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            var bookAggregate = new BookAggregate(book, _authorRepository, _genreRepository);
            bookAggregate.AddRating(ratingDto.Score, ratingDto.Review);

            await _bookRepository.UpdateAsync(book);
        }

        public async Task AddRecommendationAsync(int bookId, RecommendationDto recommendationDto)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            var bookAggregate = new BookAggregate(book, _authorRepository, _genreRepository);
            bookAggregate.AddRecommendation(recommendationDto.CustomerId, recommendationDto.Reason);

            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            Guard.AgainstInvalidId(id, ExMesConsts.ID_IS_ZERO);
            var book = await _bookRepository.GetByIdAsync(id);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            await _bookRepository.DeleteAsync(book);
        }

        public async Task DeleteRatingAsync(int bookId, int ratingId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            var rating = book.Ratings.FirstOrDefault(r => r.Id == ratingId);
            Guard.AgainstNull(rating, ExMesConsts.RATING_NOT_FOUND);

            await _bookRepository.DeleteRatingAsync(book, rating!);
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteRecommendationAsync(int bookId, int recommendationId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            var recommendation = book.Recommendations.FirstOrDefault(r => r.Id == recommendationId);
            Guard.AgainstNull(recommendation, ExMesConsts.RECOMMENDATION_NOT_FOUND);

            book.Recommendations.Remove(recommendation!);
            await _bookRepository.UpdateAsync(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            Guard.AgainstNull(books, ExMesConsts.BOOK_NOT_FOUND);

            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> GetByAuthorIdAsync(int authorId)
        {
            Guard.AgainstInvalidId(authorId, ExMesConsts.AUTHOR_NOT_FOUND);
            var book = await _bookRepository.GetByAuthorIdAsync(authorId);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            return _mapper.Map<IEnumerable<BookDto>>(book);
        }

        public async Task<IEnumerable<BookDto>> GetByGenreIdAsync(int genreId)
        {
            Guard.AgainstInvalidId(genreId, ExMesConsts.GENRE_NOT_FOUND);
            var book = await _bookRepository.GetByGenreIdAsync(genreId);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            return _mapper.Map<IEnumerable<BookDto>>(book);
        }

        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            await _bookRepository.UpdateAsync(book);
        }

        public async Task UpdateRatingAsync(int bookId, RatingDto ratingDto)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            var rating = book.Ratings.FirstOrDefault(r => r.Id == ratingDto.Id);
            Guard.AgainstNull(rating, ExMesConsts.RATING_NOT_FOUND);

            _mapper.Map(ratingDto, rating);
            await _bookRepository.UpdateAsync(book);
        }

        public async Task UpdateRecommendationAsync(int bookId, RecommendationDto recommendationDto)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            Guard.AgainstNull(book, ExMesConsts.BOOK_NOT_FOUND);

            var recommendation = book.Recommendations.FirstOrDefault(r => r.Id == recommendationDto.Id);
            Guard.AgainstNull(recommendation, ExMesConsts.RECOMMENDATION_NOT_FOUND);

            _mapper.Map(recommendationDto, recommendation);
            await _bookRepository.UpdateAsync(book);
        }
    }
}
