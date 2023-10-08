using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingCartController : ControllerBase
	{
		private readonly IShoppingCartService _shoppingCartService;
		public ShoppingCartController(IShoppingCartService shoppingCartService)
		{
			_shoppingCartService = shoppingCartService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var rs = await _shoppingCartService.GetShoppingCartsAsync();
			return Ok(rs);
		}


	}
}
