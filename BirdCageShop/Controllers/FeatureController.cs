using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.ProductFeature;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private IFeatureService _productFeatureService;

        public FeatureController(IFeatureService productFeatureService)
        {
            _productFeatureService = productFeatureService;
        }

        [HttpGet]
        [Route("get-all")]
        //[PermissionAuthorize("Admin")]
        public async Task<IActionResult> GetAllPF()
        {
            try
            {
                List<GetFeature> productFeatures = await _productFeatureService.GetAllAsync();
                return Ok(new
                {
                    Data = productFeatures
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
        //[PermissionAuthorize("Admin")]
        public async Task<IActionResult> GetPFById(int id)
        {
            try
            {
                GetFeature productFeature = await _productFeatureService.GetAsync(id);
                return Ok(new
                {
                    Data = productFeature
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

        //[PermissionAuthorize("Admin")]
        public async Task<IActionResult> CreatePF([FromBody] CreateFeature createProductFeature)
        {
            try
            {
               await _productFeatureService.CreateFeatureAsync(createProductFeature);

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
        //[PermissionAuthorize("Admin", "Customer", "Supplier", "Farmer")]
        public async Task<IActionResult> UpdatePF(int id, [FromBody] UpdateFeature updateProductFeature)
        {
            try
            {               
                await _productFeatureService.UpdateFeatureAsync(id, updateProductFeature);
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
        //[PermissionAuthorize("Admin")]
        public async Task<IActionResult> DeletePF(int id)
        {
            try
            {
                await _productFeatureService.DeleteFeatureAsync(id);
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
                List<GetFeature> productFeatures = await _productFeatureService.GetFeatureNameAsync(key);
                return Ok(new
                {
                    Data = productFeatures
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

