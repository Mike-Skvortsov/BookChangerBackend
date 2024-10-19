using BusinessLogic.ModelsDTO.ChatDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.ChatService
{
    public interface IChatService
    {
        Task<ChatDTO> GetChatByIdAsync(int chatId);
        Task<ChatDTO> CreateChatAsync(int userOneId, int userTwoId);
        Task<IEnumerable<ChatDTO>> GetUserChatsAsync(int userId);
    }
}
