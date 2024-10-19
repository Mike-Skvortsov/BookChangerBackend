using AutoMapper;
using BusinessLogic.ModelsDTO.ChatDTO;
using Database.Models;
using Database.Repositories.Interfaces;


namespace BusinessLogic.Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public async Task<ChatDTO> GetChatByIdAsync(int chatId)
        {
            var chat = await _chatRepository.GetChatByIdAsync(chatId);
            return _mapper.Map<ChatDTO>(chat);
        }

        public async Task<ChatDTO> CreateChatAsync(int userOneId, int userTwoId)
        {
            var existingChat = await _chatRepository.FindChatByParticipantsAsync(userOneId, userTwoId);
            if (existingChat != null)
            {
                return _mapper.Map<ChatDTO>(existingChat);
            }
            var chat = new Chat
            {
                UserOneId = userOneId,
                UserTwoId = userTwoId
            };

            await _chatRepository.CreateChatAsync(chat);
            return _mapper.Map<ChatDTO>(chat);
        }

        public async Task<IEnumerable<ChatDTO>> GetUserChatsAsync(int userId)
{
    var chats = await _chatRepository.GetUserChatsAsync(userId);
    return _mapper.Map<IEnumerable<ChatDTO>>(chats);
}

    }
}
