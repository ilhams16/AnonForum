using System;
using System.Collections.Generic;
using System.Text;

namespace AnonForum.BLL.DTOs.User
{
    public class CreateUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
    }
}
