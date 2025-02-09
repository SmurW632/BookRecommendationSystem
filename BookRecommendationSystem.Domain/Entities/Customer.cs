using BookRecommendationSystem.Domain.Enums;
using Microsoft.VisualBasic;

namespace BookRecommendationSystem.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Phone { get; set; }   
        public string? Email { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Навигационные свойства
        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<Recommendation> Recommendations { get; set; } = [];
    }
}
