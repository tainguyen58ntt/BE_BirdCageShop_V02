
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopUtils.UtilMethod;
using BirdCageShopViewModel.User;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Collections.Generic;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        private readonly BirdCageShopContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        
        public UserController(IUserService userService, IUnitOfWork unitOfWork, BirdCageShopContext db, UserManager<IdentityUser> userManager)
        {
            _userService = userService;
            //_unitOfWork = unitOfWork;
            _db = db;
            _userManager = userManager;

        }

        //[HttpGet]

        //public async Task<IActionResult> Get()
        //{
        //    var rs = await _userManager.Users.ToListAsync();    
        //    return Ok(rs);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetByEmailAsync([FromBody] string email)
        //{
        //    var result = await _userService.GetUserByIdAsync(id);
        //    if (result is null) return NotFound();
        //    return Ok(result);
        //}
        [HttpGet]
        [Authorize(Roles = "Staff, Admin, Manager")]
        public async Task<IActionResult> Get()
        {
            var x = await _db.ApplicationUser.ToListAsync();
          

            return Ok(x);   
        }
        [HttpGet("order-history")]
        public async Task<IActionResult> GetCustomerOrderHistory([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var x = await _userService.GetOrderHistoryAsync(pageIndex, pageSize);

            return Ok(x);
        }

        //[HttpGet("page")]
        //public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        //{
        //    if (pageIndex < 0) return BadRequest("Page index cannot be negative");
        //    if (pageSize <= 0) return BadRequest("Page size must greater than 0");
        //    var result = await _userService.GetPageAsync(pageIndex, pageSize);
        //    return Ok(result);
        //}

        //[HttpDelete("delete/{userId}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute] int userId)
        //{
        //    var user = await _userService.GetUserByIdAsync(userId);
        //    if (user == null) return BadRequest("User not exist");

        //    var result = await _userService.DeleteAsync(user);
        //    if (result is true) return Ok();
        //    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Delete Fail. Error server" });

        //}

        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterAsync([FromBody] UserSignUpViewModel vm)
        //{

        //    var isExistEmail = await _userService.IsExistsEmailAsync(vm.Email);
        //    var validateResult = await _userService.ValidateUserSignUpAsync(vm);
        //    //
        //    if (isExistEmail)
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

        //    var result = await _userService.RegisterAsync(vm);

        //    if (result is true) return Created("/api/user/register", new { message = "Register Succeed." });
        //    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Register Failed. Error Server." });
        //}



        //[HttpPut("change-password")]

        //public async Task<IActionResult> ChangePasswordAsync([FromBody] UserChangePasswordViewModel vm)
        //{
        //    var validateResult = await _userService.ValidateChangePasswordAsync(vm);
        //    if (!validateResult.IsValid)
        //    {
        //        var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
        //        return BadRequest(errors);
        //    }
        //    var result = await _userService.ChangePasswordAsync(vm);

        //    if (result is true) return Ok();
        //    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Change Password Failed. Error Server." });
        //}

    }
}
