using BookRecommendationSystem.Domain.Modules.Libraries;
using BookRecommendationSystem.Domain.Modules.Ratings;
using BookRecommendationSystem.Domain.Modules.Recommendations;
using System.ComponentModel.DataAnnotations;

namespace BookRecommendationSystem.Domain.Modules.Customers
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(20, ErrorMessage = "Поле должно содержать до 20 символов")]
        public string Nickname { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Поле Email не корректено")]
        public string Email { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Поле должно содержать до 100 символов")]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(100, ErrorMessage = "Поле должно содержать до 100 символов")]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Внешние ключи
        public int UserId { get; set; }

        //Навигационные свойства
        public ICollection<UserLibrary> Libraries { get; set; } = [];
        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<Recommendation> Recommendations { get; set; } = [];

    }
}
