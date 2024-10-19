using BusinessLogic.ModelsDTO.AuthorDTO;
using BusinessLogic.ModelsDTO.UserDTO;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.AuthorServcie
{
    public interface IAuthorService
    {
        Task<IList<AuthorForDisplayBookDTO>> GetAllAuthor();
        Task<Author> CreateAuthor(CreateAuthorDTO authorDto);
        Task<AuthorGetByIdDTO> AuthorGetById(int id);
        Task<(IList<GetAuthorsOnPageDTO>, int)> GetAuthorsPaginated(int pageNumber, int pageSize);
    }
}
