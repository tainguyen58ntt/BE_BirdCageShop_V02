using BirdCageShopInterface.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
	public class TimeService : ITimeService
	{
		public DateTime GetCurrentTime()
		{
			return DateTime.UtcNow;
		}
	}
}
