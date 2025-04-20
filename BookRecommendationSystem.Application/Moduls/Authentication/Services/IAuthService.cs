using BookRecommendationSystem.Application.Moduls.Authentication.DTOs;

namespace BookRecommendationSystem.Application.Moduls.Authentication.Services
{
    public interface IAuthService
    {
        Task<UserResponse> Register(UserRegisterDto userRegisterDto);
        Task<UserResponse> Login(UserLoginDto userLoginDto);
    }
}
