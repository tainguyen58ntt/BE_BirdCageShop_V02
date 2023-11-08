using birdcageshopinterface.IServices;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Formula;
using BirdCageShopViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IFormulaService : IBaseService
    {
        Task<List<FormulaViewModel>> GetAllAsync();
        Task<List<FormulaViewModel>> GetByIdAsync(int key);
        Task CreateFormulaAsync(CreateFormulaViewModel createFormulaViewModel);
        Task UpdateFormulaAsync(int key, UpdateFormulaViewModel updateFormulaViewModel);
        Task<Pagination<FormulaViewModel>> GetPageAsync(int pageIndex, int pageSize);
        /*Task DeleteCommentAsync(int key);*/
    }
}
