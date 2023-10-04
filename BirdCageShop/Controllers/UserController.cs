
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopUtils.UtilMethod;
using BirdCageShopViewModel.User;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rs = await _userService.GetUserAsync();
            return Ok(rs);
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _userService.GetPageAsync(pageIndex, pageSize);
            return Ok(result);
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return BadRequest("User not exist");

            var result = await _userService.DeleteAsync(user);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Delete Fail. Error server" });

        }

        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterAsync([FromBody] UserSignUpViewModel vm) 
        //{

        //    var isExistEmail = await _userService.IsExistsEmailAsync(vm.Email);
        //    var validateResult = await _userService.ValidateUserSignUpAsync(vm);
        //    //
        //    if(isExistEmail) 
        //    {
        //        validateResult.Errors.Add(new ValidationFailure("Email", "Already exist that email"));
        //    }


        //    //
        //    if (!validateResult.IsValid)
        //    {
        //        var errors = validateResult.Errors.Select(x => new { property = x.PropertyName, message = x.ErrorMessage });
        //        return BadRequest(errors);
        //    }
        //    //

        //    await _userService.
        //    return null;
        //}


    

    }
}
