using Database.Models;

namespace Database.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<(IList<Author>, int)> GetAuthorsPaginated(int pageNumber, int pageSize);
        Task<IList<Author>> GetAllAuthor();
        Task<Author> CreateAuthor(Author author);
        Task<Author> AuthorGetById(int id);
    }
}
