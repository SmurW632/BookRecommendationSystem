using BookRecommendationSystem.Application.DTOs;

namespace BookRecommendationSystem.Application.Abstractions
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllGenresAsync();
        Task<GenreDto> GetGenreByIdAsync(int id);
        Task AddGenreAsync(GenreDto genreDto);
        Task UpdateGenreAsync(GenreDto genreDto);
        Task DeleteGenreAsync(int id);
    }
}
