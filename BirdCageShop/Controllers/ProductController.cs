using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}
		[HttpGet]
		//[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Get()
		{
			var pr = await _productService.GetProductsAsync();
			return Ok(pr);
		}


		[HttpGet("page")]
		public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
		{
			if (pageIndex < 0) return BadRequest("Page index cannot be negative");
			if (pageSize <= 0) return BadRequest("Page size must greater than 0");
			var result = await _productService.GetPageAsync(pageIndex, pageSize);
			return Ok(result);
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
		{
			var result = await _productService.GetByIdAsync(id);
			if (result is null) return NotFound();
			return Ok(result);
		}
	}
}
