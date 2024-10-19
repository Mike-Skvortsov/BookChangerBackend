using BusinessLogic.ModelsDTO.AuthorDTO;
using BusinessLogic.ModelsDTO.UserDTO;
using BusinessLogic.Services.AuthorServcie;
using Database.Models;
using Database.Repositories.Implements;
using Microsoft.AspNetCore.Mvc;

namespace BooksChanger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.AuthorGetById(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO authorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _authorService.CreateAuthor(authorDto);

            if (author == null)
            {
                return BadRequest("Error creating author");
            }

            return Ok(author);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAuthor()
        {
            var author = await _authorService.GetAllAuthor();

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetAuthorsPaginated(int pageNumber = 1, int pageSize = 8)
        {
            var (authors, totalAuthors) = await _authorService.GetAuthorsPaginated(pageNumber, pageSize);

            if (!authors.Any())
            {
                return NotFound();
            }

            var response = new
            {
                TotalAuthors = totalAuthors,
                Authors = authors,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(response);
        }
    }
}
