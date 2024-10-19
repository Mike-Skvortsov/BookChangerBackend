namespace BusinessLogic.ModelsDTO.BookDTO
{
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public decimal AnnouncedPrice { get; set; }
        public string? Image { get; set; }
        public int OwnerId { get; set; }
        public string? Language { get; set; }
        public string ConditionOfTheBook { get; set; }
        public List<int> AuthorIds { get; set; }
        public List<int> GenreIds { get; set; }
    }
}
