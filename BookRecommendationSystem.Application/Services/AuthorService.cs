using AutoMapper;
using BookRecommendationSystem.Application.Abstractions;
using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Repositories;

namespace BookRecommendationSystem.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task AddAuthorAsync(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            await _authorRepository.AddAsync(author);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _authorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> GetAuthorByNameAsync(string name)
        {
            var author = await _authorRepository.GetByNameAsync(name);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task UpdateAuthorAsync(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
        await _authorRepository.UpdateAsync(author);
        }
    }
}
