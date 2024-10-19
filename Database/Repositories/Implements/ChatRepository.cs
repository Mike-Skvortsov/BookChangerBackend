using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories.Implements
{
    public class ChatRepository : IChatRepository
    {
        private readonly Context _context;

        public ChatRepository(Context context)
        {
            _context = context;
        }

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            return await _context.Chats
                .Include(c => c.Messages)
                .Include(c => c.UserOne)
                .Include(c => c.UserTwo)
                .FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task<Chat> CreateChatAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public async Task<IEnumerable<Chat>> GetUserChatsAsync(int userId)
        {
            return await _context.Chats
                .Where(c => c.UserOneId == userId || c.UserTwoId == userId)
                .Include(c => c.UserOne)
                .Include(c => c.UserTwo)
                .Include(c => c.Messages)
                .ToListAsync();
        }


        public async Task<Chat> FindChatByParticipantsAsync(int userOneId, int userTwoId)
        {
            return await _context.Chats
                .FirstOrDefaultAsync(c => (c.UserOneId == userOneId && c.UserTwoId == userTwoId) ||
                                          (c.UserOneId == userTwoId && c.UserTwoId == userOneId));
        }

    }
}
