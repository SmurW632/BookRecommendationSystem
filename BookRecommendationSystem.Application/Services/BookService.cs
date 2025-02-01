using AutoMapper;
using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Repositories;

namespace BookRecommendationSystem.Application.Services
{
    public class BookService : IBookRepository
    {
        private readonly IBookRepository _bookRepository;
        private IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public Task AddAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task AddRatingAsync(int bookId, Rating rating)
        {
            throw new NotImplementedException();
        }

        public Task AddRecommendationAsync(int bookId, Recommendation recommendation)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRatingAsync(int bookId, int ratingId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRecommendationAsync(int bookId, int recommendationId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRatingAsync(int bookId, Rating rating)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecommendationAsync(int bookId, Recommendation recommendation)
        {
            throw new NotImplementedException();
        }
    }
}
