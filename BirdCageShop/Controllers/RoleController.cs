using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.Role;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService) {
            _roleService = roleService; 
        }

        [HttpGet]   
        public async Task<IActionResult> Get()
        {
            var rs = await _roleService.GetRolesAsync();
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RoleAddViewModel vm)
        {
            var validateResult = await _roleService.ValidateRoleAddAsync(vm);
            //
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(x => new { property = x.PropertyName, message = x.ErrorMessage });
                return BadRequest(errors);
            }
            //
            var isCreated = await _roleService.CreateAsync(vm);
            if (isCreated == true) return Ok(vm);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Create failed. Server Error" });

            
        }
    }
}
