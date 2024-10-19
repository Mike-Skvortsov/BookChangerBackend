using BusinessLogic.ModelsDTO.AuthorDTO;
using BusinessLogic.ModelsDTO.GenreDTO;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.GenreService
{
    public interface IGenreService
    {

        Task<IList<GenreNameDTO>> GetAllGenre();
        Task<GenreNameDTO> GenreGetById(int id);
    }
}
