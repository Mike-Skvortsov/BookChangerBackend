using BusinessLogic.ModelsDTO.BookDTO;
using Database.Models;

namespace BusinessLogic.ModelsDTO.UserDTO
{
    public class UserById
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Description { get; set; }
        public float Rating { get; set; }
        public int NumberOfExchanges { get; set; }
        public string? Location { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime OnlineTime { get; set; }
        public virtual ICollection<GetAllBooksDTO> Books { get; set; }
        public virtual ICollection<UserReview> ReceivedReviews { get; set; } = new List<UserReview>();
    }
}
