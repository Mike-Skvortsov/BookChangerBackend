using BusinessLogic.ModelsDTO.UserDTO;
using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;


        public AuthService(IUserRepository repository, IConfiguration confguration)
        {
            _repository = repository;
            _configuration = confguration;
        }
        public async Task<UserLoginResponseDTO> Login(LoginDTO loginDTO)
        {
            var user = await _repository.GetUserByEmail(loginDTO.Email);
            if (user == null || !VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            var token = CreateToken(user);
            return new UserLoginResponseDTO { Token = token, UserId = user.Id.ToString() };
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }


    }
}
