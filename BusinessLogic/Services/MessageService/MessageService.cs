using AutoMapper;
using BusinessLogic.ModelsDTO.Message;
using Database.Models;
using Database.Repositories.Interfaces;

namespace BusinessLogic.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<MessageDTO> CreateMessageAsync(MessageDTO messageDTO)
        {
            var message = _mapper.Map<Message>(messageDTO);
            await _messageRepository.AddMessageAsync(message);
            return _mapper.Map<MessageDTO>(message);
        }
        public async Task<IEnumerable<MessageDTO>> GetChatMessagesAsync(int chatId)
        {
            var messages = await _messageRepository.GetMessagesByChatIdAsync(chatId);
            return _mapper.Map<IEnumerable<MessageDTO>>(messages);
        }

        public async Task MarkMessagesAsReadAsync(int chatId, int recipientId)
        {
            await _messageRepository.MarkMessagesAsReadAsync(chatId, recipientId);
        }

    }
}
