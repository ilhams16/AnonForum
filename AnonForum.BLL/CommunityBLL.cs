using System;
using System.Collections;
using System.Collections.Generic;
using AnonForum.BLL.DTOs.Community;
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
			_communityDAL = new CommunityDAL();
		}
		public IEnumerable<CommunityDTO> GetAll()
		{
			try
			{
				var listComm = new List<CommunityDTO>();
				var communities = _communityDAL.GetAll();
				foreach ( var community in communities )
				{
					listComm.Add(new CommunityDTO()
					{
						CommunityID = community.CommunityID,
						CommunityName = community.ComunityName,
					});
				}
				return listComm;
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message);
			}
		}
		public CommunityDTO GetbyID(int id)
		{
			var commDto = new CommunityDTO();
			try
			{
				var comm = _communityDAL.GetByID(id);
				commDto.CommunityID = comm.CommunityID;
				commDto.CommunityName = comm.ComunityName;

				return commDto;
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message);
			}
		}
	}
}
