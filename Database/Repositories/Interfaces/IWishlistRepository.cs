using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories.Interfaces
{
    public interface IWishlistRepository
    {
        Task AddWishlistAsync(Wishlist wishlist);
        Task<bool> IsInWishlist(int bookId, int userId);
        Wishlist FindWishlistItem(int bookId, int userId);
        void RemoveWishlist(Wishlist wishlist);
        Task<int> SaveChangesAsync();
        Task<List<Book>> GetWishlistBooksByUserId(int userId);
    }
}
