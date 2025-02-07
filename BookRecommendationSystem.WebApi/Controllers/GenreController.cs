using BookRecommendationSystem.Application.Abstractions;
using BookRecommendationSystem.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookRecommendationSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenreById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult> AddGenre(GenreDto genreDto)
        {
            await _genreService.AddGenreAsync(genreDto);
            return CreatedAtAction(nameof(GetGenreById), new { id = genreDto.Id }, genreDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGenre(int id, GenreDto genreDto)
        {
            if (id != genreDto.Id)
            {
                return BadRequest();
            }

            await _genreService.UpdateGenreAsync(genreDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            await _genreService.DeleteGenreAsync(id);
            return NoContent();
        }
    }
}
