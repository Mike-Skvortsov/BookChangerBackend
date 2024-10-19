using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<IList<Genre>> GetAllGenre();
        Task<Genre> GenreGetById(int id);
    }
}
