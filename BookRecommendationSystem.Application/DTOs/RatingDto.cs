namespace BookRecommendationSystem.Application.DTOs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int Score { get; set; } // Оценка от 1 до 5
        public string Review { get; set; } = null!;// Опциональный отзыв

        // Внешние ключи
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
