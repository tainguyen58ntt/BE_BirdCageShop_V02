using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
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
                List<GetSpecifications> specifications = await _specificationService.GetAllAsync();
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
                GetSpecifications productSpecification = await _specificationService.GetAsync(id);
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
        public async Task<IActionResult> CreatePF([FromBody] CreateSpecifications createSpecification)
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
        //[PermissionAuthorize("Admin", "Customer", "Supplier", "Farmer")]
        public async Task<IActionResult> UpdatePF(int id, [FromBody] UpdateSpecifications updateSpecification)
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
                List<GetSpecifications> productSpecifications = await _specificationService.GetSpecificationsNameAsync(key);
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
