
namespace BusinessLogic.ModelsDTO.AuthorDTO
{
    public class CreateAuthorDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        private DateTime? _bDay;
        public DateTime? BDay
        {
            get => _bDay;
            set => _bDay = value?.ToUniversalTime();
        }
        private DateTime? _dayOfDeath;
        public DateTime? DayOfDeath
        {
            get => _dayOfDeath;
            set => _dayOfDeath = value?.ToUniversalTime();
        }
    }

}
