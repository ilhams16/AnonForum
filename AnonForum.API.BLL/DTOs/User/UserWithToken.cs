using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonForum.API.BLL.DTOs.User
{
    public class UserWithToken
    {
        public string Username { get; set; }
        public string? Token { get; set; }
    }
}
