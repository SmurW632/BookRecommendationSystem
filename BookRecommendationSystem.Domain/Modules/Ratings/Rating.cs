using BookRecommendationSystem.Domain.Modules.Books;
using BookRecommendationSystem.Domain.Modules.Customers;
using System.ComponentModel.DataAnnotations;

namespace BookRecommendationSystem.Domain.Modules.Ratings
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Значение вне диапазона [1..5]")]
        public int Score { get; set; } // Оценка

        public string? Review { get; set; } // Опциональный отзыв

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        // Внешние ключи
        public int CustomerId { get; set; }
        public int BookId { get; set; }

        // Навигационные свойства
        public Customer? Customer { get; set; }
        public Book? Book { get; set; }

    }
}
