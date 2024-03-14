using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.BLL.DTOs.Community;
using AnonForum.BLL.DTOs.User;
using AnonForum.BLL.Interface;
using AnonForum.BO;
using AnonForum.DAL;

namespace AnonForum.BLL
{
	public class CommunityBLL : ICommunityBLL
	{
		private readonly ICommunity _communityDAL;
		public CommunityBLL()
		{
			_communityDAL = new AnonForum.DAL;
		}
		public string AddNewUser(AddUserCommunityDTO addUserCommunityDTO)
		{
			try
			{
				var newUser = new UserAuth
				{
					Username = entity.Username,
					Email = entity.Email,
					Password = entity.Password,
					Nickname = entity.Nickname,
					UserImage = entity.UserImage,
				};
				return (string)_userDAL.AddNewUser(newUser);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message);
			}
		}
	}

    internal interface ICommunity
    {
    }
}
