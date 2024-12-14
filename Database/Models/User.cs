using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public float Rating { get; set; } = 0;
        public int NumberOfExchanges { get; set; } = 0;
        public DateTime OnlineTime { get; set; } = DateTime.UtcNow;
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? Image { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
        public virtual ICollection<UserReview> SentReviews { get; set; } = new List<UserReview>();
        public virtual ICollection<UserReview> ReceivedReviews { get; set; } = new List<UserReview>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    }
}
