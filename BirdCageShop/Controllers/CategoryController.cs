using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopViewModel.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rs = await _categoryService.GetCategoriesAsync();
            return Ok(rs);
        }

        [HttpPost]
        
        public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateViewModel vm)
        {
            var validateResult = await _categoryService.ValidateCategoryCreateAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(x => new { property = x.PropertyName, message = x.ErrorMessage });
                return BadRequest(errors);
            }
            //check if exist name category
            var isExistNameCategory = await _categoryService.isExistNameCategory(vm.CategoryName);
            if (isExistNameCategory) return StatusCode(StatusCodes.Status409Conflict, new { message = "That category name already exists" });


            var result = await _categoryService.CreateAsync(vm);
            if (result is true) return Created("/api/category", vm);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Create category failed. Server Error." });
        }
    }
}
