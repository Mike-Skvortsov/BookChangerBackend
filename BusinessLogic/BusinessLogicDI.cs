using BusinessLogic.Services.AuthorServcie;
using BusinessLogic.Services.AuthService;
using BusinessLogic.Services.BookService;
using BusinessLogic.Services.ChatService;
using BusinessLogic.Services.GenreService;
using BusinessLogic.Services.MessageService;
using BusinessLogic.Services.UserService;
using BusinessLogic.Services.WishlistService;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class BusinessLogicDI
    {
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IWishlistService, WishlistService>();

        }
    }
}
