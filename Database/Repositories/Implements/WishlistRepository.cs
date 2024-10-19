using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implements
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly Context _context;

        public WishlistRepository(Context context)
        {
            _context = context;
        }

        public async Task AddWishlistAsync(Wishlist wishlist)
        {
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsInWishlist(int bookId, int userId)
        {
            return await _context.Wishlists.AnyAsync(w => w.BookId == bookId && w.UserId == userId);
        }
        public Wishlist FindWishlistItem(int bookId, int userId)
        {
            return _context.Wishlists.FirstOrDefault(w => w.BookId == bookId && w.UserId == userId);
        }

        public void RemoveWishlist(Wishlist wishlist)
        {
            _context.Wishlists.Remove(wishlist);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<List<Book>> GetWishlistBooksByUserId(int userId)
        {
            return await _context.Wishlists
                                 .Where(w => w.UserId == userId)
                                 .Include(m => m.Book.Authors)
                                 .ThenInclude(ba => ba.Author)
                                 .Select(w => w.Book)
                                 .ToListAsync();
        }
    }
}
