using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopViewModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

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

        //get product by bird type
        [HttpGet("by-birdCage-type/{birdCageTypeId}")]
        public async Task<IActionResult> GetByBirdCageType([FromRoute] int birdCageTypeId, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var pr = await _productService.GetByBirdCageTypePageAsync(birdCageTypeId, pageIndex, pageSize);
            return Ok(pr);
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryAsync([FromRoute] int categoryId, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _productService.GetByCagegoryTypePageAsync(categoryId, pageIndex, pageSize);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _productService.GetPageAsync(pageIndex, pageSize);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product is null) return NotFound();
            var result = await _productService.RemoveAsync(product);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Delete product failed. Server Error." });
        }

     

        [HttpGet("search-by-title")]
        public async Task<IActionResult> GetProductByTitle(string title, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
          
            var result = await _productService.GetByTilePageAsync(title, pageIndex, pageSize);
            return Ok(result);
        }


        //[HttpGet("view-feedback/{productId}")]
        //public async Task<IActionResult> GetFeedBackByIdAsync([FromRoute] int productId)
        //{
        //    var product = await _productService.GetProductByIdAsync(productId);
        //    if (product is null) return NotFound("Not found this product");
        //    var result = await _productService.GetFeedBackByProductId(productId);
        //    return Ok(result);
        //}


        [HttpPost("review-product/{productId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ReviewProductByIdAsync([FromRoute] int productId, [FromBody] AddReviewProductViewModel addReviewProductViewModel)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product is null) return NotFound("Not found this product");

            // create
            bool isSuccess = await _productService.AddReviewProduct(productId, addReviewProductViewModel);

            if (isSuccess) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Review product failed. Server Error." });
        }


        



        //
        [HttpGet("from-wishlist")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetFromWishlistAsync()
        {
            var result = await _productService.GetProductsFromWishlistAsync();
            return Ok(result);
        }



        [HttpPost("add-to-wishlist/{productId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToWishlistAsync([FromRoute] int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product is null) return BadRequest(new { property = "Product ID", message = "Product doesn't exist." });
            var result = await _productService.AddToWishlistAsync(productId);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Add to wishlist failed. Server Error." });
        }


        [HttpDelete("remove-product-from-wishlist/{productId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RemoveProductFromWishlistAsync([FromRoute] int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product is null) return BadRequest(new { property = "Product ID", message = "Product doesn't exist." });
            var result = await _productService.RemoveProductFromWishlistAsync(productId);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Remove product from wishlist failed. Server Error." });
        }

        [HttpPost("add-to-cart-from-wishlist")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddtoCartFromWishlistFromWishlistAsync()
        {
            var result = await _productService.MoveProFromWishlistToShoppingCart();
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Move product from wishlist to shopping cart failed. Server Error." });
        }













    }
}
