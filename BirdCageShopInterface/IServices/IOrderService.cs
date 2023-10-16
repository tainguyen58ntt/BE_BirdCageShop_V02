using BirdCageShopDbContext.Models;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Product;
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
        Task<bool> GetByIdToUpdateStatusToProcessAsync(int id); // where  order: approved, payment: cod  or  payonline-approved
        Task<Pagination<OrderWithDetailViewModel>> GetOrderByOderStatusPageAsync(int orderStatusId, int pageIndex, int pageSize);
        //test
        Task<Order?> GetOrderByIdAsync(int id);
        //test
        //Task<Pagination<OrderWithDetailViewModel>> GetByOrderStatusPageAsync(int statusId, int pageIndex, int pageSize);
    }
}
