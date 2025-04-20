using BookRecommendationSystem.Domain.Modules.Books;
using System.ComponentModel.DataAnnotations;

namespace BookRecommendationSystem.Domain.Modules.Authors
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Имя не может содержать более 50-ти символов")]
        public string Name { get; set; } = string.Empty;
        public string? Biography { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public ICollection<Book> Books { get; set; } = [];
    }
}
