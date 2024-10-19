using AutoMapper;
using BusinessLogic.ModelsDTO.GenreDTO;
using Database.Repositories.Interfaces;

namespace BusinessLogic.Services.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GenreNameDTO> GenreGetById(int id)
        {
            var genre = _mapper.Map<GenreNameDTO>(await _repository.GenreGetById(id));
            return genre;
        }
        public async Task<IList<GenreNameDTO>> GetAllGenre()
        {
            var genre = _mapper.Map<IList<GenreNameDTO>>(await _repository.GetAllGenre());
            return genre;
        }
    }
}
