using BookRecommendationSystem.Application.Moduls.Authentication.DTOs;
using BookRecommendationSystem.Application.Moduls.Authentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookRecommendationSystem.WebApi.Controllers
{
    public class AccountController(IAuthService authService) : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var result = await authService.Login(userLoginDto);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var result = await authService.Register(userRegisterDto);

            return Ok(result);
        }
    }
}
