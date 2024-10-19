
namespace BusinessLogic.ModelsDTO.Message
{
    public class MessageDTO
    {
        public string Content { get; set; }
        public int SenderId { get; set; }
        public bool IsRead { get; set; }
        public int ChatId { get; set; }
        public int ReceiverId { get; set; }  

    }
}
