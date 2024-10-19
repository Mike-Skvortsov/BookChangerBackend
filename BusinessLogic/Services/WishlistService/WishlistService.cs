using AutoMapper;
using BusinessLogic.ModelsDTO.BookDTO;
using BusinessLogic.ModelsDTO.WishlistDTO;
using Database.Models;
using Database.Repositories.Interfaces;


namespace BusinessLogic.Services.WishlistService
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IMapper _mapper;

        public WishlistService(IWishlistRepository wishlistRepository, IMapper mapper)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
        }

        public async Task AddWishlistAsync(WishlistCreateDTO wishlistDto, int userId)
        {
            var wishlist = _mapper.Map<Wishlist>(wishlistDto);
            wishlist.UserId = userId;
            await _wishlistRepository.AddWishlistAsync(wishlist);
        }
        public async Task<bool> IsInWishlist(int bookId, int userId)
        {
            return await _wishlistRepository.IsInWishlist(bookId, userId);
        }
        public async Task<bool> RemoveWishlistAsync(int bookId, int userId)
        {
            var wishlist = _wishlistRepository.FindWishlistItem(bookId, userId);
            if (wishlist != null)
            {
                _wishlistRepository.RemoveWishlist(wishlist);
                await _wishlistRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<BooksForBookcaseDTO>> GetWishlistBooks(int userId)
        {
            var books = await _wishlistRepository.GetWishlistBooksByUserId(userId);
            return _mapper.Map<List<BooksForBookcaseDTO>>(books);
        }
    }
}
