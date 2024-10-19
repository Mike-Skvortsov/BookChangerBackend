using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ModelsDTO.BookDTO
{
    public class BooksForBookcaseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorNames { get; set; } 
        public decimal AnnouncedPrice { get; set; }
        public string Image { get; set; }
    }
}
