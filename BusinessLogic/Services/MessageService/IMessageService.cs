using BusinessLogic.ModelsDTO.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.MessageService
{
    public interface IMessageService
    {
        Task<MessageDTO> CreateMessageAsync(MessageDTO messageDTO);
        Task<IEnumerable<MessageDTO>> GetChatMessagesAsync(int chatId);
        Task MarkMessagesAsReadAsync(int chatId, int recipientId);
    }
}
