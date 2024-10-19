
namespace Database.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }
        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; }
        public bool IsRead { get; set; }
        public virtual Chat Chat { get; set; }
        public int ChatId { get; set; }

    }
}
