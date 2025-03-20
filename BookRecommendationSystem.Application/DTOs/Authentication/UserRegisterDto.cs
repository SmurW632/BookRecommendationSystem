namespace BookRecommendationSystem.Application.DTOs.Authentication
{
    public record class UserRegisterDto(string Username, string Email, string Phone, string Password);
}
