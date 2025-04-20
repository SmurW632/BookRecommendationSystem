using BookRecommendationSystem.Domain.Modules.Authors;
using Microsoft.EntityFrameworkCore;

namespace BookRecommendationSystem.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
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

        public async Task DeleteAsync(Author author)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author?> GetByNameAsync(string name)
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}
