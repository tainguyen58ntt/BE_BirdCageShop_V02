using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.Specification;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationController : ControllerBase
    {
        private ISpecificationService _specificationService;

        public SpecificationController(ISpecificationService productSpecificationsService)
        {
            _specificationService = productSpecificationsService;
        }

        [HttpGet]
        [Route("get-all")]
        //[PermissionAuthorize("Admin")]
        public async Task<IActionResult> GetAllPF()
        {
            try
            {
                List<GetSpecification> specifications = await _specificationService.GetAllAsync();
                return Ok(new
                {
                    Data = specifications
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
                GetSpecification productSpecification = await _specificationService.GetAsync(id);
                return Ok(new
                {
                    Data = productSpecification
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
        public async Task<IActionResult> CreatePF([FromBody] CreateSpecification createSpecification)
        {
            try
            {
                await _specificationService.CreateSpecificationAsync(createSpecification);

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
        public async Task<IActionResult> UpdatePF(int id, [FromBody] UpdateSpecification updateSpecification)
        {
            try
            {
                await _specificationService.UpdateSpecificationAsync(id, updateSpecification);
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
                await _specificationService.DeleteSpecificationAsync(id);
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
                List<GetSpecification> productSpecifications = await _specificationService.GetSpecificationNameAsync(key);
                return Ok(new
                {
                    Data = productSpecifications
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
