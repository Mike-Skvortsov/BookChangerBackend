using BusinessLogic.ModelsDTO.BookDTO;
using BusinessLogic.Services.BookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net.Sockets;
using System.Security.Claims;

namespace BooksChanger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        [Authorize]

        [HttpPost("create")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO bookDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID is missing from the token claims.");
            }
            var userId = int.Parse(userIdClaim.Value);
            var book = await _bookService.CreateBook(bookDto, userId);

            if (book == null)
            {
                return BadRequest("Error creating book");
            }

            return Ok(book);

        }
        [Authorize]

        [HttpGet("myBooks")]
        public async Task<IActionResult> GetMyBooks()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID is missing from the token claims.");
            }
            var userId = int.Parse(userIdClaim.Value);
            var books = await _bookService.GetMyBooks(userId);

            if (books == null || !books.Any())
            {
                return NotFound("No books found for the user.");
            }
            return Ok(books);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooksByTitle(string query)
        {
            var books = await _bookService.SearchBooksByTitle(query);

            if (!books.Any())
            {
                return NotFound("No books found with the given title fragment.");
            }

            return Ok(books);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? genreIds, [FromQuery] string? titleQuery, [FromQuery] string? sortPrice, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            List<int> genreIdList = null;
            if (!string.IsNullOrEmpty(genreIds))
            {
                genreIdList = genreIds.Split(',').Select(int.Parse).ToList();
            }

            var books = await _bookService.GetBooks(pageNumber, pageSize, genreIdList, titleQuery, sortPrice, minPrice, maxPrice);
            int totalPages = await _bookService.GetTotalPages(pageSize, genreIdList, titleQuery, minPrice, maxPrice);
            Response.Headers.Add("X-Total-Pages", totalPages.ToString());

            return Ok(books);
        }


        [Authorize]

        [HttpDelete("delete/{bookId:int}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var book = await _bookService.GetBookById(bookId);
            if(book == null)
            {
                return NotFound("Book not found");
            }
            try
            {
                await _bookService.DeleteBook(bookId);
                return Ok($"Book has been successfully deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while deleting the book: {ex.Message}");
            }
        }
        [HttpGet("topBooks")]
        public async Task<IActionResult> GetTopBooks([FromQuery] int count = 4) // Default to 4 books if no parameter is provided
        {
            var books = await _bookService.GetTopBooks(count);
            return Ok(books);
        }

    }
}
