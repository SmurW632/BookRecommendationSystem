using AutoMapper;
using BookRecommendationSystem.Application.Abstractions;
using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Application.Helpers;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.ExceptionMessageConsts;
using BookRecommendationSystem.Domain.Repositories;

namespace BookRecommendationSystem.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task AddGenreAsync(GenreDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);
            Guard.AgainstNull(genre, ExMesConsts.GENRE_NOT_FOUND);

            await _genreRepository.AddAsync(genre);
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            Guard.AgainstNull(genre, ExMesConsts.GENRE_NOT_FOUND);

            await _genreRepository.DeleteAsync(genre!);
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            Guard.AgainstNull(genres, ExMesConsts.GENRE_NOT_FOUND);

            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public async Task<GenreDto> GetGenreByIdAsync(int id)
        {
            Guard.AgainstInvalidId(id, ExMesConsts.ID_IS_ZERO);
            var genre = await _genreRepository.GetByIdAsync(id);
            Guard.AgainstNull(genre, ExMesConsts.GENRE_NOT_FOUND);

            return _mapper.Map<GenreDto>(genre);
        }

        public async Task UpdateGenreAsync(GenreDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);
            Guard.AgainstNull(genre, ExMesConsts.GENRE_NOT_FOUND);

            await _genreRepository.UpdateAsync(genre);
        }
    }
}
