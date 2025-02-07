using AutoMapper;
using BookRecommendationSystem.Application.Abstractions;
using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Application.Helpers;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.ExceptionMessageConsts;
using BookRecommendationSystem.Domain.Repositories;

namespace BookRecommendationSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            Guard.AgainstNull(user, ExMesConsts.USER_NOT_FOUND);

            await _userRepository.AddAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            Guard.AgainstNull(user, ExMesConsts.USER_NOT_FOUND);

            await _userRepository.DeleteAsync(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            Guard.AgainstNull(users, ExMesConsts.USER_NOT_FOUND);

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            Guard.AgainstNull(user, ExMesConsts.USER_NOT_FOUND);

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            Guard.AgainstNull(user, ExMesConsts.USER_NOT_FOUND);

            await _userRepository.UpdateAsync(user);
        }
    }
}
