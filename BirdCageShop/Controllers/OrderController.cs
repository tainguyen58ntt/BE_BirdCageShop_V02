using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _orderService.GetPaginationAsync(pageIndex, pageSize);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _orderService.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

    }
}
