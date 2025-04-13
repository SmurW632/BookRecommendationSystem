namespace BookRecommendationSystem.Domain.Entities
{
    public class Recommendation
    {
        public int Id { get; set; }
        public string Reason { get; set; } = null!;
        public decimal Score { get; set; } // Вес рекомендации (например, от 0 до 1)
        public string AlgorithmType { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Внешние ключи
        public int? CustomerId { get; set; }
        public int BookId { get; set; }

        // Навигационные свойства
        public Customer? Customer { get; set; }
        public Book Book { get; set; } = null!;
    }
}
