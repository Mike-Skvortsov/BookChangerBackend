using Database.Models;

namespace Database.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesByChatIdAsync(int chatId);
        Task<Message> AddMessageAsync(Message message);
        Task MarkMessagesAsReadAsync(int chatId, int recipientId);
    }
}
