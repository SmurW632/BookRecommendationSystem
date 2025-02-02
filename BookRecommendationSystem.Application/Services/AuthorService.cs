using AutoMapper;
using BookRecommendationSystem.Application.Abstractions;
using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Application.Helpers;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.ExceptionMessageConsts;
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
            Guard.AgainstNull(author, ExMesConsts.AUTHOR_NOT_FOUND);

            await _authorRepository.AddAsync(author);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            Guard.AgainstInvalidId(id, ExMesConsts.ID_IS_ZERO);
            var author = await _authorRepository.GetByIdAsync(id);
            Guard.AgainstNull(author, ExMesConsts.AUTHOR_NOT_FOUND);

            await _authorRepository.DeleteAsync(author!);
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            Guard.AgainstNull(authors, ExMesConsts.AUTHOR_NOT_FOUND);

            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            Guard.AgainstInvalidId(id, ExMesConsts.ID_IS_ZERO);
            var author = await _authorRepository.GetByIdAsync(id);
            Guard.AgainstNull(author, ExMesConsts.AUTHOR_NOT_FOUND);

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> GetAuthorByNameAsync(string name)
        {
            var author = await _authorRepository.GetByNameAsync(name);
            Guard.AgainstNull(author, ExMesConsts.AUTHOR_NOT_FOUND);

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task UpdateAuthorAsync(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            Guard.AgainstNull(author, ExMesConsts.AUTHOR_NOT_FOUND);

            await _authorRepository.UpdateAsync(author);
        }
    }
}
