using BusinessLogic.Services.AuthorServcie;
using BusinessLogic.Services.GenreService;
using Microsoft.AspNetCore.Mvc;

namespace BooksChanger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGenre()
        {
            var genre = await _genreService.GetAllGenre();

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }


    }
}
