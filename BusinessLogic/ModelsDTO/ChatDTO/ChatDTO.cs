using BusinessLogic.ModelsDTO.Message;
using BusinessLogic.ModelsDTO.UserDTO;

namespace BusinessLogic.ModelsDTO.ChatDTO
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public UserForChatsDTO UserOne { get; set; }
        public UserForChatsDTO UserTwo { get; set; }
        public List<MessageDTO>? Messages { get; set; }
    }
}
