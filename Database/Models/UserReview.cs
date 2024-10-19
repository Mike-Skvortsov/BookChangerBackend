namespace Database.Models
{
    public class UserReview:Review
    {
        public int SenderId { get; set; }
        public virtual User? Sender { get; set; }
        public int RecipientId { get; set; }
        public virtual User? Recipient { get; set; }
    }
}
