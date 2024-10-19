using BusinessLogic.ModelsDTO.AuthorDTO;
using BusinessLogic.ModelsDTO.GenreDTO;
using BusinessLogic.ModelsDTO.UserDTO;

namespace BusinessLogic.ModelsDTO.BookDTO
{
    public class GetBookByIdDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }    
        public int PageCount { get; set; }
        public decimal AnnouncedPrice { get; set; }
        public string ConditionOfTheBook { get; set; }
        public string? Image { get; set; }
        public string? Language { get; set; }
        public UserForBookGetById Owner { get; set; }
        public ICollection<AuthorForDisplayBookDTO>? Authors { get; set; }
        public ICollection<GenreNameDTO>? Genres { get; set; }
    }
}
