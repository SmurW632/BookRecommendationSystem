using BookRecommendationSystem.Domain.Modules.Books;
using BookRecommendationSystem.Domain.Modules.Customers;
using System.ComponentModel.DataAnnotations;

namespace BookRecommendationSystem.Domain.Modules.Recommendations
{
    public class Recommendation
    {
        public int Id { get; set; }
        [Required]
        public string Reason { get; set; } = string.Empty;
        [Required]
        [Range(0, 1, ErrorMessage = "Значение поля в не диапазона [0..1]")]
        public float Score { get; set; } // Вес рекомендации

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [DataType(DataType.DateTime)]
        public DateTime? ExpiresAt { get; set; }

        public bool IsActive { get; set; } = true;

        // Внешние ключи
        public int? CustomerId { get; set; }
        public int BookId { get; set; }

        // Навигационные свойства
        public Customer? Customer { get; set; }
        public Book? Book { get; set; }
    }
}
