namespace BusinessLogic.ModelsDTO.UserDTO
{
    public class UserForBookGetById
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public float Rating { get; set; }
        public DateTime OnlineTime { get; set; } = DateTime.Now;
        public string Image { get; set; }

    }
}
