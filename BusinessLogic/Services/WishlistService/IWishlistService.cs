using BusinessLogic.ModelsDTO.BookDTO;
using BusinessLogic.ModelsDTO.WishlistDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.WishlistService
{
    public interface IWishlistService
    {
        Task AddWishlistAsync(WishlistCreateDTO wishlistDto, int userId);
        Task<bool> IsInWishlist(int bookId, int userId);
        Task<bool> RemoveWishlistAsync(int bookId, int userId);
        Task<List<BooksForBookcaseDTO>> GetWishlistBooks(int userId);
    }
}
