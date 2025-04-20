using BookRecommendationSystem.Domain.Modules.Books;
using System.ComponentModel.DataAnnotations;

namespace BookRecommendationSystem.Domain.Modules.Genres
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Поле не может содержать более 100 символов")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Навигационное свойство
        public ICollection<BookGenre> BookGenres { get; set; } = [];
    }
}
