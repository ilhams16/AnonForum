using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace AnonForum.BLL.DTOs.User
{
    public class CreateUserDTO
    {
		[Required(ErrorMessage = "Username is required")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }
        public string Nickname { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Please confirm your password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }
		[Display(Name = "Profile Image")]
		public IFormFile ImageFile { get; set; }
		public string UserImage { get; set; }
	}
}
