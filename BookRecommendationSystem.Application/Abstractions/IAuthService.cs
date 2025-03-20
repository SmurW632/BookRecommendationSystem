using BookRecommendationSystem.Application.DTOs.Authentication;

namespace BookRecommendationSystem.Application.Abstractions
{
    public interface IAuthService
    {
        Task<UserResponse> Register(UserRegisterDto userRegisterDto);
        Task<UserResponse> Login(UserLoginDto userLoginDto);
    }
}
