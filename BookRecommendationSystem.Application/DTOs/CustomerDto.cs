using BookRecommendationSystem.Domain.Enums;

namespace BookRecommendationSystem.Application.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}