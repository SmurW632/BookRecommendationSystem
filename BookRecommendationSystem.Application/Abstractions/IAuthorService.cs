using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Application.Abstractions
{
    public interface IAuthorService
    {
        Task<AuthorDto> GetAuthorByIdAsync(int id);
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto> GetAuthorByNameAsync(string name);
        Task AddAuthorAsync(AuthorDto author);
        Task UpdateAuthorAsync(AuthorDto author);
        Task DeleteAuthorAsync(int id);
    }
}
