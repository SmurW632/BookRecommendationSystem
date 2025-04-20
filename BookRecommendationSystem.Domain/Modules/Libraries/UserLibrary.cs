using BookRecommendationSystem.Domain.Modules.Books;
using BookRecommendationSystem.Domain.Modules.Customers;
using BookRecommendationSystem.Domain.Modules.Libraries.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookRecommendationSystem.Domain.Modules.Libraries
{
    public class UserLibrary
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public bool IsFavorite { get; set; } = false;

        [Required]
        public ReadingStatus ReadingStatus { get; set; } = ReadingStatus.Unknown;

        // Внешние ключи
        public int CustomerId { get; set; }
        public int BookId { get; set; }

        // Навигационные свойства
        public Customer? Customer { get; set; }
        public Book? Book { get; set; }
    }
}
