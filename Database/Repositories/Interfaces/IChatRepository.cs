using Database.Models;

namespace Database.Repositories.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> GetChatByIdAsync(int chatId);
        Task<IEnumerable<Chat>> GetUserChatsAsync(int userId);
        Task<Chat> CreateChatAsync(Chat chat);
        Task<Chat> FindChatByParticipantsAsync(int userOneId, int userTwoId);
    }
}
