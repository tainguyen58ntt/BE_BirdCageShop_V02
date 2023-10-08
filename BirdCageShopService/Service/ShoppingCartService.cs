using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ShoppingCart;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
	public class ShoppingCartService : BaseService, IShoppingCartService
	{
		public ShoppingCartService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
		{
		}

		public async Task<IEnumerable<ShoppingCartViewModel>> GetShoppingCartsAsync()
		{
			//

			var currentUserId = _claimService.GetCurrentUserId();
			if (currentUserId == -1) return new List<ShoppingCartViewModel>();
			var result = await _unitOfWork.ShoppingCartRepository.GetShoppingCartsAsync(currentUserId);
			return _mapper.Map<List<ShoppingCartViewModel>>(result);
		}
	}
}
