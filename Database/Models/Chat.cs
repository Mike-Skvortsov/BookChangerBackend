namespace Database.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int UserOneId { get; set; }
        public virtual User UserOne { get; set; }
        public int UserTwoId { get; set; }
        public virtual User UserTwo { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
