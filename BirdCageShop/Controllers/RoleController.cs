using BirdCageShopInterface.IServices;
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
    }
}
