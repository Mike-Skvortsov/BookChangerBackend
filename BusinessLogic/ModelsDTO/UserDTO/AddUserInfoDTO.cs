using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ModelsDTO.UserDTO
{
    public class AddUserInfoDTO
    {
        public string? UserName { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Location { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? CurrentPassword { get; set; }
        public string? Password { get; set; }
    }

}
