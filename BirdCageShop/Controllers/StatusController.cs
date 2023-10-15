using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService  _tes;
        public StatusController(IStatusService tes)
        {
            _tes = tes;
         
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rs = await _tes.GetStatusByIdAsync(1);
            return Ok(rs);
        }
    }
}
