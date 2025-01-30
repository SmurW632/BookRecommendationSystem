namespace BookRecommendationSystem.Domain.Entities
{
    public class Recommendation
    {
        public int Id { get; set; }
        public float Score { get; set; } // Вес рекомендации (например, от 0 до 1)

        // Внешние ключи
        public int UserId { get; set; }
        public int BookId { get; set; }

        // Навигационные свойства
        public User User { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
