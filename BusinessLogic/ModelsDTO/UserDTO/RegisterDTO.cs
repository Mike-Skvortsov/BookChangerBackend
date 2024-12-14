﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ModelsDTO.UserDTO
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; } = 1;

    }

}
