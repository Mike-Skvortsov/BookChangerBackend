using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implements
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly Context _context;

        public AuthorRepository(Context context)
        {
            _context = context;
        }
        public async Task<Author> AuthorGetById(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .ThenInclude(ba => ba.Book)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return null;
            }

            return author;
        }
        public async Task<IList<Author>> GetAllAuthor()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<(IList<Author>, int)> GetAuthorsPaginated(int pageNumber, int pageSize)
        {
            var query = _context.Authors
                .OrderBy(a => a.Name)
                .Select(x => x).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var authors = await query.ToListAsync();
            var totalAuthors = await _context.Authors.CountAsync();

            return (authors, totalAuthors);
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }
    }
}
