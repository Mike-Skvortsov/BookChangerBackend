using BusinessLogic.ModelsDTO.UserDTO;
using Database.Models;

namespace BusinessLogic.Services.UserService
{
    public interface IUserService
    {
        Task<bool> ChangePassword(int userId, string currentPassword, string newPassword);
        Task<bool> DeleteUser(int userId);
        Task<bool> UpdateUser(int userId, AddUserInfoDTO userUpdateDTO);
        Task<User> CreateUser(RegisterDTO userDTO);
        Task<UserById> UserGetById(int id);
    }
}
