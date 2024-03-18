using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.BLL.DTOs.Community;
using AnonForum.BLL.DTOs.Post;
using AnonForum.BO;

namespace AnonForum.BLL.Interface
{
	public interface ICommunityBLL
	{
		IEnumerable<CommunityDTO> GetAll();
		CommunityDTO GetbyID(int id);
	}
}
