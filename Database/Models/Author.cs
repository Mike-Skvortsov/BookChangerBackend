namespace Database.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public DateTime BDay { get; set; }
        public DateTime? DayOfDeath { get; set; }
        public virtual ICollection<BookAuthor> Books { get; set; } = new List<BookAuthor>();
    }
}