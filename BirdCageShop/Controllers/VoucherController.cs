using BirdCageShopInterface.IRepositories;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVourcherService _vourcherService;
        public VoucherController(IVourcherService vourcherService)
        {
            _vourcherService = vourcherService;
        }

        

        //get all vourcher exp date > current date
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rs = await _vourcherService.GetVourcherAsync();
            return Ok(rs);
        }
    }
}
