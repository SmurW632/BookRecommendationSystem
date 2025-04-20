namespace BookRecommendationSystem.Application.Moduls.Authors.Services
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
