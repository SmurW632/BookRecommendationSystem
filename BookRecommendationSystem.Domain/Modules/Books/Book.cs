using BookRecommendationSystem.Domain.Modules.Authors;
using BookRecommendationSystem.Domain.Modules.Libraries;
using BookRecommendationSystem.Domain.Modules.Ratings;
using BookRecommendationSystem.Domain.Modules.Recommendations;
using System.ComponentModel.DataAnnotations;

namespace BookRecommendationSystem.Domain.Modules.Books
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Название должно быть не менее 5 и не более 50 символов", MinimumLength = 5)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public int PublishedYear { get; set; }

        [DataType(DataType.ImageUrl, ErrorMessage = "Поле должно содержать ссылку")]
        public string CoverImageUrl { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Внешние ключи
        public int AuthorId { get; set; }

        // Навигационные свойства
        public Author? Author { get; set; }
        public ICollection<UserLibrary> UserLibraries { get; set; } = [];
        public ICollection<BookGenre> Genres { get; set; } = [];
        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<Recommendation> Recommendations { get; set; } = [];
    }
}
