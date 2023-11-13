using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Design;
using BirdCageShopViewModel.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IDesignService
    {
/*        Task<List<DesignViewModel>> GetAllAsync();*/
        Task<List<DesignViewModel>> GetByIdAsync(string key);
        Task CreateDesinAsync(CreateDesign createDesign);
        Task DeleteDesignAsync(string key);
        /*Task UpdateFormulaAsync(int key, UpdateFormulaViewModel updateFormulaViewModel);
        Task<Pagination<FormulaViewModel>> GetPageAsync(int pageIndex, int pageSize);*/
    }
}
