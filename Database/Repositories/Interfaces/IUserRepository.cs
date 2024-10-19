using Database.Models;

namespace Database.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UpdateUser(User user);
        Task<User> UserGetById(int id);
        Task<User> CreateUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<bool> DeleteUser(int userId);
    }
}
