using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories.Implements
{
    public class BookRepository : IBookRepository
    {
        private readonly Context _context;

        public BookRepository(Context context)
        {
            _context = context;
        }

        public async Task<Book> BookGetById(int id)
        {
            return await _context.Books
                .Include(b => b.Owner)
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Genres)
                    .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> CreateBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<IEnumerable<Book>> SearchBooksByTitle(string titleSubstring)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(titleSubstring))
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByUserId(int userId)
        {
            return await _context.Books
                .Where(b => b.OwnerId == userId)
                .Include(b => b.Authors).ThenInclude(ba => ba.Author)
                .Include(b => b.Genres)
                .ToListAsync();
        }
        public async Task DeleteBook(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Book>> GetBooks(int pageNumber, int pageSize, List<int>? genreIds, string? titleQuery, string? sortPrice, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Books
                .AsNoTracking()
                .Include(b => b.Authors).ThenInclude(a => a.Author)
                .Include(b => b.Owner)
                .Include(b => b.Genres)
                .Where(b => (genreIds == null || genreIds.Count == 0 || b.Genres.Any(g => genreIds.Contains(g.GenreId))) &&
                            (string.IsNullOrEmpty(titleQuery) || b.Title.Contains(titleQuery)) &&
                            (!minPrice.HasValue || b.AnnouncedPrice >= minPrice) &&
                            (!maxPrice.HasValue || b.AnnouncedPrice <= maxPrice));

            if (!string.IsNullOrEmpty(sortPrice))
            {
                query = sortPrice == "asc" ? query.OrderBy(b => b.AnnouncedPrice) : query.OrderByDescending(b => b.AnnouncedPrice);
            }

            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetTotalBookCount(List<int>? genreIds, string? titleQuery, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Books.AsQueryable()
                .Where(b => (genreIds == null || genreIds.Count == 0 || b.Genres.Any(g => genreIds.Contains(g.GenreId))) &&
                            (string.IsNullOrEmpty(titleQuery) || b.Title.Contains(titleQuery)) &&
                            (!minPrice.HasValue || b.AnnouncedPrice >= minPrice) &&
                            (!maxPrice.HasValue || b.AnnouncedPrice <= maxPrice));

            return await query.CountAsync();
        }
        public async Task<IEnumerable<Book>> GetTopBooks(int count)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Authors).ThenInclude(a => a.Author)
                .Include(b => b.Owner)
                .Include(b => b.Genres)
                .OrderByDescending(b => b.Id) 
                .Take(count)
                .ToListAsync();
        }


    }
}
