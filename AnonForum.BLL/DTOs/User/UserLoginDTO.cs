using System;
using System.Collections.Generic;
using System.Text;

namespace AnonForum.BLL.DTOs.User
{
    public class UserLoginDTO
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
