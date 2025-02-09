namespace BookRecommendationSystem.Application.DTOs
{
    public class RecommendationDto
    {
        public int Id { get; set; }
        public string Reason { get; set; } = null!;
        public float Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        // Внешние ключи
        public int CustomerId { get; set; }
        public int BookId { get; set; }
    }
}
