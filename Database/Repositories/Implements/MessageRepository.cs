using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implements
{
    public class MessageRepository : IMessageRepository
    {
        private readonly Context _context;

        public MessageRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(int chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task<Message> AddMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }
        public async Task MarkMessagesAsReadAsync(int chatId, int recipientId)
        {
            var messages = await _context.Messages
                .Where(m => m.ChatId == chatId && m.ReceiverId == recipientId && !m.IsRead)
                .ToListAsync();

            if (messages.Any())
            {
                foreach (var message in messages)
                {
                    message.IsRead = true;
                }

                await _context.SaveChangesAsync();
            }
        }

    }
}
