
using AutoMapper;
using BusinessLogic.ModelsDTO.BookDTO;
using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;


namespace BusinessLogic.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository,IGenreRepository genreRepository,IMapper mapper, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
            _userRepository= userRepository;
        }

        public async Task<GetBookByIdDTO> GetBookById(int id)
        {
            var book = _mapper.Map<GetBookByIdDTO>(await _bookRepository.BookGetById(id));

            return book;
        }

        public async Task<GetBookByIdDTO> CreateBook(CreateBookDTO createdBook, int userId)
        {
            Book book = _mapper.Map<Book>(createdBook);
            book.DateTime = DateTime.UtcNow;
            book.OwnerId = userId;
            book.Owner = await _userRepository.UserGetById(userId);

            foreach (var authorId in createdBook.AuthorIds)
            {
                var author = await _authorRepository.AuthorGetById(authorId);
                if (author != null)
                {
                    book.Authors.Add(new BookAuthor { AuthorId = author.Id, Book = book });
                }
                else
                {
                    throw new Exception($"Author with ID {authorId} not found.");
                }
            }

            foreach (var genreId in createdBook.GenreIds)
            {
                var genre = await _genreRepository.GenreGetById(genreId);
                if (genre != null)
                {
                    book.Genres.Add(new BookGenre { GenreId = genre.Id, Book = book });
                }
                else
                {
                    throw new Exception($"Genre with ID {genreId} not found.");
                }
            }

            await _bookRepository.CreateBook(book);
            var bookDetail = _mapper.Map<GetBookByIdDTO>(book);

            return bookDetail;
        }


        public async Task<IEnumerable<SearchBookDTO>> SearchBooksByTitle(string titleSubstring)
        {
            var books = await _bookRepository.SearchBooksByTitle(titleSubstring);
            return _mapper.Map<IEnumerable<SearchBookDTO>>(books);
        }
        public async Task DeleteBook(int bookId)
        {
            await _bookRepository.DeleteBook(bookId);
        }

        public async Task<IEnumerable<BooksForBookcaseDTO>> GetMyBooks(int userId)
        {
            var books = await _bookRepository.GetBooksByUserId(userId);
            return _mapper.Map<IEnumerable<BooksForBookcaseDTO>>(books);
        }

        public async Task<IEnumerable<GetAllBooksDTO>> GetBooks(int pageNumber, int pageSize, List<int>? genreIds, string? titleQuery, string? sortPrice, decimal? minPrice, decimal? maxPrice)
        {
            var bookEntities = await _bookRepository.GetBooks(pageNumber, pageSize, genreIds, titleQuery, sortPrice, minPrice, maxPrice);
            return _mapper.Map<IEnumerable<GetAllBooksDTO>>(bookEntities);
        }

        public async Task<int> GetTotalPages(int pageSize, List<int>? genreIds, string? titleQuery, decimal? minPrice, decimal? maxPrice)
        {
            int totalBooks = await _bookRepository.GetTotalBookCount(genreIds, titleQuery, minPrice, maxPrice);
            return (int)Math.Ceiling((double)totalBooks / pageSize);
        }
        public async Task<IEnumerable<GetAllBooksDTO>> GetTopBooks(int count)
        {
            var books = await _bookRepository.GetTopBooks(count);
            return _mapper.Map<IEnumerable<GetAllBooksDTO>>(books);
        }


    }
}