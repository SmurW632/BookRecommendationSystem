using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Repositories;
using BookRecommendationSystem.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BookRecommendationSystem.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private const string AUTHOR_NOT_FOUND = "Автор не найден.";

        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Author author)
        {
            await _context.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = await GetByIdAsync(id);

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            Guard.AgainstNull(author, AUTHOR_NOT_FOUND);

            return author!;
        }

        public async Task<Author> GetByNameAsync(string name)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Name == name);
            Guard.AgainstNull(author, AUTHOR_NOT_FOUND);

            return author!;
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}
