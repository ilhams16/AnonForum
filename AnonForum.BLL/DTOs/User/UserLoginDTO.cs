using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnonForum.BLL.DTOs.User
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Please enter your username or email.")]
        public string UsernameOrEmail { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }
    }
}
