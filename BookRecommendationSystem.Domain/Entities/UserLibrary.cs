using BookRecommendationSystem.Domain.Enums;

namespace BookRecommendationSystem.Domain.Entities
{
    public class UserLibrary
    {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public bool IsFavorite { get; set; }
        public ReadingStatus ReadingStatus { get; set; } = ReadingStatus.Unknown;

        public int CustomerId { get; set; }
        public int BookId { get; set; }

        public Customer? Customer { get; set; }
        public Book? Book { get; set; }
    }
}
