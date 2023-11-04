using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.BirdCageType;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdCageTypeController : ControllerBase
    {
        private IBirdCageTypeService _birdCageTypeService;

        public BirdCageTypeController(IBirdCageTypeService birdCageTypeService)
        {
            _birdCageTypeService = birdCageTypeService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllPF()
        {
            try
            {
                List<GetBirdCageType> birdCageTypes = await _birdCageTypeService.GetAllAsync();
                return Ok(new
                {
                    Data = birdCageTypes
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPFById(int id)
        {
            try
            {
                GetBirdCageType birdCageType = await _birdCageTypeService.GetAsync(id);
                return Ok(new
                {
                    Data = birdCageType
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

        public async Task<IActionResult> CreatePF([FromBody] CreateBirdCageType createBirdCageType)
        {
            try
            {
                await _birdCageTypeService.CreateBirdCageTypeAsync(createBirdCageType);

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


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePF(int id, [FromBody] UpdateBirdCageType updateBirdCageType)
        {
            try
            {
                await _birdCageTypeService.UpdateBirdCageTypeAsync(id, updateBirdCageType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePF(int id)
        {
            try
            {
                await _birdCageTypeService.DeleteBirdCageTypeAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetByPFName(string key)
        {
            try
            {
                List<GetBirdCageType> birdCageTypes = await _birdCageTypeService.GetBirdCageTypeNameAsync(key);
                return Ok(new
                {
                    Data = birdCageTypes
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