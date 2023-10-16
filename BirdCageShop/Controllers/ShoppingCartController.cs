using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface.IRepositories;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.ShoppingCart;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IVourcherService _vourcherService;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IVourcherService vourcherService)
        {
            _shoppingCartService = shoppingCartService;
            _vourcherService = vourcherService;
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var shoppingCarts = await _shoppingCartService.GetShoppingCartsAsync();

            decimal total = 0;
            foreach (var cart in shoppingCarts)
            {

                //cart.pricePerUnit = GetPriceBasedOnQuantity(cart);
                //total += (cart.pricePerUnit * cart.Count);
                total += cart.ProductViewModel.PriceAfterDiscount * cart.Count;
            }


            var rs = new
            {

                shoppingCarts = shoppingCarts,
                total = total
            };
            return Ok(rs);
        }


        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //	//var rs = await _shoppingCartService.GetShoppingCartsAsync();
        //	ShoppingCartWithOrderVM shoppingCartWithOrderVM;
        //	shoppingCartWithOrderVM = new()
        //	{
        //		ShoppingCartList = await _shoppingCartService.GetShoppingCartsAsync(),

        //              Order = new()
        //          };

        //          foreach (var cart in shoppingCartWithOrderVM.ShoppingCartList)
        //          {
        //              //cart.Product.ProductImages = productImages.Where(u => u.ProductId == cart.Product.Id).ToList();
        //              //cart.pricePerUnit = GetPriceBasedOnQuantity(cart);
        //              shoppingCartWithOrderVM.Order.TotalPrice += (cart.pricePerUnit * cart.Count);
        //          }
        //          return Ok(shoppingCartWithOrderVM);
        //}

        //      // get cart by specific by user id

        //      //
        ////
        [HttpPost("update-cart/{productId}")]  // use for update and add
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
            if (existProduct == false) return BadRequest(new { property = "Product ID", message = "Product doesn't exist in cart" });

            var result = await _shoppingCartService.RemoveFromCartAsync(productId);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Remove from cart failed. Server Error." });

        }



        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(ConfirmOrderAddViewModel confirmOrderAddViewModel)
        {
            var validateResult = await _shoppingCartService.ValidateConfirmOrderAddAsync(confirmOrderAddViewModel);



            // check voucher code
            // if != null and invalid return badrequest
            if (confirmOrderAddViewModel.VourcherCode != null)
            {
                var voucher = await _vourcherService.GetVourcherByCodeAsync(confirmOrderAddViewModel.VourcherCode);
                if (voucher == null)
                {
                    validateResult.Errors.Add(new ValidationFailure("Voucher", "This voucher is not valid."));
                }
            }
            // if == null ok


            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(x => new { property = x.PropertyName, message = x.ErrorMessage });
                return BadRequest(errors);
            }

            var result = await _shoppingCartService.CheckoutAsync(confirmOrderAddViewModel);
            if (result is true) return Ok();
            // checkout 


            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Check out from cart failed. Server Error." });
        }

      

        

    }
}
