using BirdCageShopInterface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IRoleService _roleService;
		public ProductController(IRoleService roleService)
		{
			_roleService = roleService;
		}
	}
}
