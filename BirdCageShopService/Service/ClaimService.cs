using BirdCageShopInterface.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
	public class ClaimService : IClaimService
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public ClaimService(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public int GetCurrentUserId()
		{
			var id = _contextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
			return id is null ? -1 : int.Parse(id);
		}

		public int GetRoleId()
		{
			var id = _contextAccessor.HttpContext?.User?.FindFirst("RoleId")?.Value;
			return id is null ? -1 : int.Parse(id);
		}
	}
}
