using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ModelsDTO.BookDTO
{
    public class SearchBookDTO
    {
        public string Title { get; set; }
        public string Rating { get; set; }
        public decimal AnnouncedPrice { get; set; }
        public string? Image { get; set; }
        public string AuthorsName { get; set; }
    }
}
