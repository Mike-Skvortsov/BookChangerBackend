namespace Database.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<BookGenre> Books { get; set; } = new List<BookGenre>(); 

    }
}
