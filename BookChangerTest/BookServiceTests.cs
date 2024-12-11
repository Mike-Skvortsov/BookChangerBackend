using Xunit;
using Moq;
using System.Threading.Tasks;
using BusinessLogic.ModelsDTO.BookDTO;
using Database.Models;
using Database.Repositories.Interfaces;
using BusinessLogic.Services.BookService;
using AutoMapper;
using BusinessLogic.ModelsDTO.UserDTO;

namespace BusinessLogic.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task GetBookById_ShouldReturnCorrectBook()
        {
            // Arrange
            var mockBookRepository = new Mock<IBookRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockAuthorRepository = new Mock<IAuthorRepository>();
            var mockGenreRepository = new Mock<IGenreRepository>();

            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                AnnouncedPrice = 50.0m,
                Description = "Test Description",
                PageCount = 100,
                ConditionOfTheBook = "New",
                Language = "English",
                Owner = new User { Id = 1, UserName = "John Doe" }
            };

            var expectedDto = new GetBookByIdDTO
            {
                Title = "Test Book",
                AnnouncedPrice = 50.0m,
                Description = "Test Description",
                PageCount = 100,
                ConditionOfTheBook = "New",
                Language = "English",
                Image = "test-image-url",
                Owner = new UserForBookGetById { Id = 1, UserName = "John Doe" }
            };

            mockBookRepository.Setup(repo => repo.BookGetById(1)).ReturnsAsync(book);
            mockMapper.Setup(mapper => mapper.Map<GetBookByIdDTO>(book)).Returns(expectedDto);

            var bookService = new BookService(
                mockBookRepository.Object,
                mockAuthorRepository.Object,
                mockGenreRepository.Object,
                mockMapper.Object,
                mockUserRepository.Object
            );

            // Act
            var result = await bookService.GetBookById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.Title, result.Title);
            Assert.Equal(expectedDto.AnnouncedPrice, result.AnnouncedPrice);
            Assert.Equal(expectedDto.Description, result.Description);
            Assert.Equal(expectedDto.PageCount, result.PageCount);
            Assert.Equal(expectedDto.ConditionOfTheBook, result.ConditionOfTheBook);
            Assert.Equal(expectedDto.Language, result.Language);
            Assert.Equal(expectedDto.Image, result.Image);
            Assert.Equal(expectedDto.Owner.Id, result.Owner.Id);
        }
    }
}
