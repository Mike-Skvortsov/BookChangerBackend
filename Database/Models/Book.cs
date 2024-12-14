using System.Text.Json.Serialization;

namespace Database.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int PageCount { get; set; }
        public decimal AnnouncedPrice { get; set; }
        public byte[]? Image { get; set; }
        public string ConditionOfTheBook { get; set; }
        public int OwnerId { get; set; }
        public string? Language { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public virtual User? Owner { get; set; }
        [JsonIgnore]
        public virtual ICollection<BookAuthor> Authors { get; set;} = new List<BookAuthor>();
        [JsonIgnore]
        public virtual ICollection<BookGenre> Genres { get; set; } = new List<BookGenre>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

    }
}
