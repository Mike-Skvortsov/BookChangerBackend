using BusinessLogic.ModelsDTO.UserDTO;

namespace BusinessLogic.ModelsDTO.BookDTO
{
    public class GetAllBooksDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal AnnouncedPrice { get; set; }
        public string? Image { get; set; }
        public string AuthorNames { get; set; }
        public OwnerDTO Owner { get; set; }
    }
}
