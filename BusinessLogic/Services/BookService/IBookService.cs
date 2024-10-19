
using BusinessLogic.ModelsDTO.BookDTO;
using Database.Models;

namespace BusinessLogic.Services.BookService
{
    public interface IBookService
    {
        Task DeleteBook(int bookId);
        Task<IEnumerable<BooksForBookcaseDTO>> GetMyBooks(int userId);
        Task<GetBookByIdDTO> CreateBook(CreateBookDTO bookDto, int userId);
        Task<GetBookByIdDTO> GetBookById(int id);
        Task<IEnumerable<SearchBookDTO>> SearchBooksByTitle(string titleSubstring);
        Task<IEnumerable<GetAllBooksDTO>> GetBooks(int pageNumber, int pageSize, List<int>? genreIds, string? titleQuery, string? sortPrice, decimal? minPrice, decimal? maxPrice);
        Task<int> GetTotalPages(int pageSize, List<int>? genreIds, string? titleQuery, decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<GetAllBooksDTO>> GetTopBooks(int count);
    }
}