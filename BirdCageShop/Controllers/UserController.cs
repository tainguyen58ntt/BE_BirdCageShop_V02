
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopReposiory;
using BirdCageShopService.Service;
using BirdCageShopUtils.UtilMethod;
using BirdCageShopViewModel.Auth;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly ITimeService _timeService;


        private readonly BirdCageShopContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(RoleManager<IdentityRole> roleManager, ITimeService timeService, IOrderService orderService, IUserService userService, IUnitOfWork unitOfWork, BirdCageShopContext db, UserManager<IdentityUser> userManager)
        {
            _userService = userService;
            //_unitOfWork = unitOfWork;
            _db = db;
            _userManager = userManager;
            _orderService = orderService;
            _timeService = timeService;
            _roleManager = roleManager;
        }
   

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            //var result = await _userService.GetUserByIdAsync(id);
            var result = await _db.ApplicationUser.FirstOrDefaultAsync(x => x.Id == id);
            if (result is null) return BadRequest("Not found that user");

            result.Role = _userManager.GetRolesAsync(result).GetAwaiter().GetResult().FirstOrDefault();
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
        //[Authorize(Roles = "Staff, Admin, Manager")]
        public async Task<IActionResult> Get()
        {
            List<ApplicationUser> objUserList = await _db.ApplicationUser.ToListAsync();
            foreach (var user in objUserList)
            {

                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();


            }
            var rs = await _userManager.Users.ToListAsync();
            return Ok(objUserList);


        }
        [HttpGet("order-history")]
        public async Task<IActionResult> GetCustomerOrderHistory([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var x = await _userService.GetOrderHistoryAsync(pageIndex, pageSize);

            return Ok(x);
        }

        [HttpGet("vouhers")]
        public async Task<IActionResult> GetVouherOfUser([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var x = await _userService.GetVoucherByUserIdAsync(pageIndex, pageSize);

            return Ok(x);
        }


        [HttpPut("cancel-order/{orderId}")]
        public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
        {
            var x = await _orderService.GetOrderByIdAsync(orderId);
            if (x.OrderStatus == "Pending" && x.PaymentStatus == "Payonline")
            {
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = x.PaymentIntentId
                };

                var service = new RefundService();
                Refund refund = service.Create(options);
                x.OrderStatus = "Canceled";
                x.PaymentStatus = "Refund";
            }
            if (x.OrderStatus == "Pending" && x.PaymentStatus == "COD")
            {
                //var options = new RefundCreateOptions
                //{
                //    Reason = RefundReasons.RequestedByCustomer,
                //    PaymentIntent = x.PaymentIntentId
                //};

                //var service = new RefundService();
                //Refund refund = service.Create(options);
                x.OrderStatus = "Canceled";
                x.PaymentStatus = "Canceled";
            }
            await _orderService.UpdateOrderAsync(x);

            return Ok();


        }




        [HttpDelete("/{userId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return BadRequest("User not exist");

            var result = await _userService.DeleteAsync(user);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Delete Fail. Error server" });

        }

        [HttpPut("recover/{userId}")]
        public async Task<IActionResult> RecoverAsync([FromRoute] string userId)
        {
            var user = await _userService.GetUserIncludeUserDeletedByIdAsync(userId);
            if (user == null) return BadRequest("User not exist");

            var result = await _userService.DeleteAsync(user);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Delete Fail. Error server" });

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserSignUpViewModel registerUser)
        {


            //check exists
            var userExists = await _userManager.FindByEmailAsync(registerUser.Email);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "User already exists" });
            }
            // check password
            var validateResult = await _userService.ValidateUserSignUpAsync(registerUser);

            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(x => new { property = x.PropertyName, message = x.ErrorMessage });
                return BadRequest(errors);
            }


            //test
            var user2 = CreateUser();
            user2.UserName = registerUser.UserName;
            user2.Email = registerUser.Email;
            user2.CreatedAt = _timeService.GetCurrentTimeInVietnam();
            user2.SecurityStamp = Guid.NewGuid().ToString();
            //user2.TwoFactorEnabled = true;
            //test


            if (await _roleManager.RoleExistsAsync("Customer"))
            {
                var result = await _userManager.CreateAsync(user2, registerUser.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user2, "Customer");

                    //Add Token to Verify the email....
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user2);
                    //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user2.Email }, Request.Scheme);
                    //var message = new Message(new string[] { user2.Email! }, "Confirmation email link", confirmationLink!);
                    //_emailService.SendEmail(message);

                    //    return StatusCode(StatusCodes.Status200OK,
                    //new Response { Status = "Success", Message = $"User created & Email Sent to {user2.Email} SuccessFully" });

                    return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Success", Message = $"Register success" });

                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User failed to create" });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "This role dosent exists" });
            }

            //
            return Ok();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        [HttpPut("change-password")]

        public async Task<IActionResult> ChangePasswordAsync([FromBody] UserChangePasswordViewModel vm)
        {

            var validateResult = await _userService.ValidateChangePasswordAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            var result = await _userService.ChangePasswordAsync(vm);

            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Change Password Failed. Error Server." });
        }
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateProfileViewModel vm)
        {

            // check valid 
            var validateResult = await _userService.ValidateUpdateProfileAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }

            // check user name
            bool checkUsername = await _userService.isExistUsername(vm.UserName);
            if (checkUsername)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Already exist that username. Error Server." });
            }


            // check email
            bool checkEmail = await _userService.isExistUsername(vm.Email);
            if (checkUsername)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Already exist that email. Error Server." });
            }

            var result = await _userService.UpdateProfileAsync(vm);

            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Update Profile Failed. Error Server." });
        }
    }
}
