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
		private readonly IUser _userDAL;
		public CommunityBLL()
		{
			_communityDAL = new CommunityDAL();
			_userDAL = new UserDAL();
		}
		public void AddNewUser(AddUserCommunityDTO addUserCommunityDTO)
		{
			try
			{
				_userDAL.add
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
