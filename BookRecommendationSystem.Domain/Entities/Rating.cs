namespace BookRecommendationSystem.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; } // Оценка от 1 до 5
        public string? Review { get; set; } // Опциональный отзыв
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

    }
}
