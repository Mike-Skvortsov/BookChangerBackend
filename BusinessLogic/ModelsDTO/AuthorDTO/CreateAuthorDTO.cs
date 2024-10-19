
namespace BusinessLogic.ModelsDTO.AuthorDTO
{
    public class CreateAuthorDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public DateTime BDay { get; set; }
        public DateTime? DayOfDeath { get; set; }
    }
}
