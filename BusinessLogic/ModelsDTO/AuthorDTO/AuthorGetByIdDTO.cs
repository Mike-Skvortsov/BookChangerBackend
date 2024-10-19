using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ModelsDTO.AuthorDTO
{
    public class AuthorGetByIdDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime BDay { get; set; }
        public DateTime DayOfDeath { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }

}
