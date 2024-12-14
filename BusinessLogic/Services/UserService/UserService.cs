using AutoMapper;
using Database.Repositories.Interfaces;
using Database.Models;
using BusinessLogic.ModelsDTO.UserDTO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Database.Repositories.Implements;

namespace BusinessLogic.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;


        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<UserById> UserGetById(int id)
        {
            var user = _mapper.Map<UserById>(await _repository.UserGetById(id));

            return user;
        }
        public async Task<User> CreateUser(RegisterDTO userDTO, int role = 1)
        {
            User user = _mapper.Map<User>(userDTO);
            CreatePassHash(userDTO.Password, out byte[] passHash, out byte[] passSalt);
            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            user.RoleId = role;
            return await _repository.CreateUser(user);
        }

        private void CreatePassHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using var hmac = new HMACSHA512();
            passSalt = hmac.Key;
            passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<bool> UpdateUser(int userId, AddUserInfoDTO userUpdateDTO)
        {
            var user = await _repository.UserGetById(userId);
            if (user == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(userUpdateDTO.Image))
            {
                string pattern = @"^data:image\/[a-zA-Z]+;base64,";
                if (Regex.IsMatch(userUpdateDTO.Image, pattern))
                {
                    userUpdateDTO.Image = Regex.Replace(userUpdateDTO.Image, pattern, "");
                }

                try
                {
                    user.Image = Convert.FromBase64String(userUpdateDTO.Image);
                }
                catch (FormatException ex)
                {
                    return false;
                }
            }


            _mapper.Map(userUpdateDTO, user);
            return await _repository.UpdateUser(user);
        }
        public async Task<bool> ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var user = await _repository.UserGetById(userId);
            if (user == null || !VerifyPasswordHash(currentPassword, user.PasswordHash, user.PasswordSalt))
            {
                return false;
            }

            CreatePassHash(newPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);
            user.PasswordHash = newPasswordHash;
            user.PasswordSalt = newPasswordSalt;
            return await _repository.UpdateUser(user);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                return await _repository.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false; 
            }
        }

    }
}
