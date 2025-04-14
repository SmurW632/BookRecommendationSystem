namespace BookRecommendationSystem.Application.DTOs.Authentication
{
    public class UpdateProfileDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
    }
}
