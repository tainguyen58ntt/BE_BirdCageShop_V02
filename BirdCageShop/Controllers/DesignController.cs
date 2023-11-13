using BirdCageShopDomain.Models;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopViewModel.BirdCageType;
using BirdCageShopViewModel.Design;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignController : Controller
    {
        private IDesignService _designService;

        public DesignController(IDesignService designService)
        {
            _designService = designService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPFById(string id)
        {
            try
            {
                List<DesignViewModel> designViewModels = await _designService.GetByIdAsync(id);
                return Ok(new
                {
                    Data = designViewModels
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }


        [HttpPost]
        [Route("create")]

        public async Task<IActionResult> CreatePF([FromBody] CreateDesign createBirdCageType)
        {
            try
            {
                await _designService.CreateDesinAsync(createBirdCageType);

                return Ok(new
                {
                    StatusCode = 200,

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }
    }
}
