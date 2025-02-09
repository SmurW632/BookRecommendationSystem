namespace BookRecommendationSystem.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; } // Оценка от 1 до 5
        public string Review { get; set; } = null!;// Опциональный отзыв

        // Внешние ключи
        public int CustomerId { get; set; }
        public int BookId { get; set; }

        // Навигационные свойства
        public Customer Customer { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
