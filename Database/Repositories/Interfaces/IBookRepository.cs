using Database.Models;

namespace Database.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task DeleteBook(int bookId);
        Task<IEnumerable<Book>> GetBooksByUserId(int userId);
        public Task<Book> BookGetById(int id);
        Task<Book> CreateBook(Book book);
        Task<IEnumerable<Book>> SearchBooksByTitle(string titleSubstring);
        Task<IEnumerable<Book>> GetBooks(int pageNumber, int pageSize, List<int>? genreIds, string? titleQuery, string? sortPrice, decimal? minPrice, decimal? maxPrice);
        Task<int> GetTotalBookCount(List<int>? genreIds, string? titleQuery, decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<Book>> GetTopBooks(int count);
    }
}
