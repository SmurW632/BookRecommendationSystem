using BookRecommendationSystem.Domain.Enums;
using Microsoft.VisualBasic;

namespace BookRecommendationSystem.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public int UserId { get; set; }

        //Навигационные свойства
        public UserEntity User { get; set; } = null!;
        public ICollection<UserLibrary> Libraries{ get; set; } = [];
        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<Recommendation> Recommendations { get; set; } = [];
    }
}
