namespace Database.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public float Rating { get; set; }
    }
}
