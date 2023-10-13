using BirdCageShopDbContext.Models;
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

        // get cart by specific by user id

        //
		//
        [HttpPost("add-to-cart/{productId}")]  // use for update and add
        public async Task<IActionResult> AddItemFromShoppingCart([FromRoute] int productId, [FromQuery] int count)
		{
            // check exist product in db 
            var existProduct = await _shoppingCartService.ExistProductAsync(productId);
            if (existProduct == null) return BadRequest(new { property = "Product ID", message = "Product does not exist" });
            // check if pro exist in cart  -> update count == count
            var existProductCart = await _shoppingCartService.CreateOrUpdateAsync(productId, count);
            if (existProductCart == true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Add to cart failed. Server Error" });

        }
        // delete product in cart
        [HttpDelete("remove-from-cart/{productId}")]
        public async Task<IActionResult> RemoveItemFromShoppingCart([FromRoute] int productId)
		{
            var existProduct = await _shoppingCartService.ExistProductByIdAndUserIdAsync(productId);	
			if(existProduct == false) return BadRequest(new { property = "Product ID", message = "Product doesn't exist in cart" });

            var result = await _shoppingCartService.RemoveFromCartAsync(productId);
            if (result is true) return Ok();	
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Remove from cart failed. Server Error." });

        }

        // 

        //[HttpPost("checkout")]
        //public async Task<IActionResult> Checkout()
        //{
        //    var result = await _shoppingCartService.CheckoutFromCartAsync();
        //    if (result is true) return Ok();

        //    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Cehck out from cart failed. Server Error." });
        //}

    }
}
