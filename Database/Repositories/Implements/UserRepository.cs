using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }
        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> UserGetById(int id)
        {
            return await _context.Users.Include(x => x.Books)
                .Include(x => x.ReceivedReviews)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<User> CreateUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                var saveResult = await _context.SaveChangesAsync();
                if (saveResult > 0)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Логування помилок
                Console.WriteLine($"Error while creating user: {ex.Message}");
                return null;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
