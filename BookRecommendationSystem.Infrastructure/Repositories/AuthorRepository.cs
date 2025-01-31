using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookRecommendationSystem.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public readonly AppDbContext _context;
        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Author author)
        {
            await _context.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var author = GetByIdAsync(id).Result;
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<Author> GetByNameAsync(string name)
        {
            return await _context.Authors.FirstAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}
