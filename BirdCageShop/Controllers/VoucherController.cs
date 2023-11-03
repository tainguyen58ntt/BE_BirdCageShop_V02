using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using BirdCageShopInterface.IServices;
using BirdCageShopOther.Email;
using BirdCageShopService.Service;
using BirdCageShopViewModel.Voucher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVourcherService _vourcherService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public VoucherController(IVourcherService vourcherService, IUserService userService, IEmailService emailService)
        {
            _vourcherService = vourcherService;
            _userService = userService;
            _emailService = emailService;
        }



        //get all vourcher exp date > current date
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rs = await _vourcherService.GetVourcherAsync();
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] VourcherAddViewModel vm)
        {
            ApplicationUser applicationUser = null;
            if (vm.ApplicationUserId != null)
            {
                // check exist user
                applicationUser = await _userService.GetUserByIdAsync(vm.ApplicationUserId);
                if (applicationUser == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Not found that user. Error Server." });
                }
            }
            // check valid model
            var validateResult = await _vourcherService.ValidateVourcherAdddpAsync(vm);


            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(x => new { property = x.PropertyName, message = x.ErrorMessage });
                return BadRequest(errors);
            }

            // add
            var result = await _vourcherService.CreateNewAsync(vm);


            if (result is true)
            {
                // send email
                var message = new Message(new string[] { applicationUser.Email! }, "Voucher", "You got new voucher, bought something to use this voucher");
                _emailService.SendEmail(message);
                return Created("/api/voucher", new { message = "Created Succeed." });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Create Failed. Error Server." });
        }
    }
}
