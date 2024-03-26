using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace AnonForum.API.BLL.DTOs.User
{
    public class CreateUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IFormFile File { get; set; }
        public string? UserImage { get; set; }
    }
}
