using BusinessLogic.ModelsDTO.UserDTO;
using Database.Models;

namespace BusinessLogic.Services.AuthService
{
    public interface IAuthService
    {
        Task<UserLoginResponseDTO> Login(LoginDTO loginDTO);
        public string CreateToken(User user);
    }
}
