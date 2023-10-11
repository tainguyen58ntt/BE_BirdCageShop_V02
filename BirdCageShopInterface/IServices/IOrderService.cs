using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IOrderService : IBaseService
	{
    
        Task<Pagination<OrderWithDetailViewModel>> GetPaginationAsync(int pageIndex, int pageSize);
        Task<OrderWithDetailViewModel?> GetByIdAsync(int id);
    }
}
