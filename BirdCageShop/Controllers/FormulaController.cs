using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.Formula;
using Microsoft.AspNetCore.Mvc;

namespace BirdCageShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormulaController : ControllerBase
    {
        private IFormulaService _formulaService;

        public FormulaController(IFormulaService formulaService)
        {
            _formulaService = formulaService;
        }
        [HttpGet("page")]
        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _formulaService.GetPageAsync(pageIndex, pageSize);
            return Ok(result);
        }
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllPF()
        {
            try
            {
                List<FormulaViewModel> formulaViewModels = await _formulaService.GetAllAsync();
                return Ok(new
                {
                    Data = formulaViewModels
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
        public async Task<IActionResult> GetPFById(int id)
        {
            try
            {
                List<FormulaViewModel> formulaViewModels = await _formulaService.GetByIdAsync(id);
                return Ok(new
                {
                    Data = formulaViewModels.ToList()
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
        public async Task<IActionResult> CreatePF([FromForm] CreateFormulaViewModel createFormulaViewModel)
        {
            try
            {
                await _formulaService.CreateFormulaAsync(createFormulaViewModel);

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
        public async Task<IActionResult> UpdatePF(int id, [FromBody] UpdateFormulaViewModel updateFormulaViewModel)
        {
            try
            {
                await _formulaService.UpdateFormulaAsync(id, updateFormulaViewModel);
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

    }
}

