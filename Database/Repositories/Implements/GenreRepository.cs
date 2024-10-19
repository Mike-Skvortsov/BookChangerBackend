using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories.Implements
{
    public class GenreRepository : IGenreRepository
    {
        private readonly Context _context;
        public GenreRepository(Context context)
        {
            _context = context;
        }
        public async Task<Genre> GenreGetById(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return null;
            }

            return genre;
        }
        public async Task<IList<Genre>> GetAllGenre()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}
