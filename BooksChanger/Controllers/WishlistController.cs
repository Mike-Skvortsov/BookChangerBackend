using BusinessLogic.ModelsDTO.WishlistDTO;
using BusinessLogic.Services.WishlistService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooksChanger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost]
        [Authorize]  
        public async Task<IActionResult> AddBookToWishlist(WishlistCreateDTO wishlistDto)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                await _wishlistService.AddWishlistAsync(wishlistDto, userId);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpGet("check/{bookId}")]
        [Authorize]  
        public async Task<IActionResult> CheckWishlist(int bookId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var isInWishlist = await _wishlistService.IsInWishlist(bookId, userId);
                return Ok(new { isInWishlist });
            }
            return Unauthorized();
        }

        [HttpDelete("{bookId}")]
        [Authorize]  
        public async Task<IActionResult> RemoveBookFromWishlist(int bookId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var success = await _wishlistService.RemoveWishlistAsync(bookId, userId);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            return Unauthorized();
        }
        [HttpGet("books")]
        [Authorize]
        public async Task<IActionResult> GetWishlistBooks()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var books = await _wishlistService.GetWishlistBooks(userId);
            return Ok(books);
        }
    }
}
