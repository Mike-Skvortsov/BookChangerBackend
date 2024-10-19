using AutoMapper;
using BusinessLogic.ModelsDTO.AuthorDTO;
using BusinessLogic.ModelsDTO.GenreDTO;
using BusinessLogic.ModelsDTO.UserDTO;
using Database.Models;
using Database.Repositories.Interfaces;

namespace BusinessLogic.Services.AuthorServcie
{
    public class AuthorService: IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AuthorGetByIdDTO> AuthorGetById(int id)
        {
            var author = _mapper.Map<AuthorGetByIdDTO>(await _repository.AuthorGetById(id));
            return author;
        }

        public async Task<Author> CreateAuthor(CreateAuthorDTO authorDto)
        {
            Author author = _mapper.Map<Author>(authorDto);
            if (!string.IsNullOrWhiteSpace(authorDto.Image))
            {
                try
                {
                    author.Image = Convert.FromBase64String(authorDto.Image);
                }
                catch (FormatException)
                {
                    throw new Exception("Not valid image");
                }
            }

            return await _repository.CreateAuthor(author);
        }
        public async Task<(IList<GetAuthorsOnPageDTO>, int)> GetAuthorsPaginated(int pageNumber, int pageSize)
        {
            var (authors, totalAuthors) = await _repository.GetAuthorsPaginated(pageNumber, pageSize);
            var authorDTOs = _mapper.Map<IList<GetAuthorsOnPageDTO>>(authors);
            return (authorDTOs, totalAuthors);
        }
        public async Task<IList<AuthorForDisplayBookDTO>> GetAllAuthor()
        {
            var author = _mapper.Map<IList<AuthorForDisplayBookDTO>>(await _repository.GetAllAuthor());
            return author;
        }
    }
}
