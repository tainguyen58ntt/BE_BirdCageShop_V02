using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.Feature;
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
        [HttpGet]
        [Route("get-all-formula")]
        public async Task<IActionResult> GetAllFormula()
        {
            try
            {
                List<FormulaViewModel> formulaViewModels = await _formulaService.GetAllFromulaAsync();
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

        [HttpGet("get-by-birdcagetypeId/{id}")]
        public async Task<IActionResult> GetPFById(int id)
        {
            try
            { 
                List<FormulaViewModel> formulaViewModels = await _formulaService.GetByIdAsync(id); // get by birdcagetype id
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
        [HttpGet("get-by-formulaId/{formulaId}")] 
        //[PermissionAuthorize("Admin")]
        public async Task<IActionResult> GetFormulaById(int formulaId)
        {
            try
            {
                FormulaViewModel productFeature = await _formulaService.GetFormulaById(formulaId);
                return Ok(new
                {
                    Data = productFeature
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
        public async Task<IActionResult> UpdatePF(int id, [FromForm] UpdateFormulaViewModel updateFormulaViewModel)
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
        [HttpDelete("{id}")]
        //[PermissionAuthorize("Admin")]
        public async Task<IActionResult> DeleteFormula(int id)
        {
            try
            {
                await _formulaService.DeleteFormula(id);
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
        [HttpPut("recover-formula/{id}")]
        //[PermissionAuthorize("Admin")]
        public async Task<IActionResult> RecoverFormula(int id)
        {
            try
            {
                await _formulaService.UpdateFormula(id);
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

