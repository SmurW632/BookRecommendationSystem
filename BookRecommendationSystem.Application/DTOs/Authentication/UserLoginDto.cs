namespace BookRecommendationSystem.Application.DTOs.Authentication
{
    public record class UserLoginDto(string Username, string Email, string Phone, string Password);
}
