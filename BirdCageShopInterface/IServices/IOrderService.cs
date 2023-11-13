using BirdCageShopDbContext.Models;
using birdcageshopinterface.IServices;
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
        Task<bool> GetByIdToUpdateStatusToApprovedAsync(int id); 
        Task<bool> GetByIdToUpdateStatusToShippeddAsync(int id); 


        Task<bool> GetByIdToUpdateStatusPayToApprovedAsync(int id); // where  order: shipped, payment: cod | -> approved
        Task<Pagination<OrderWithDetailViewModel>> GetOrderByOderStatusPageAsync(int orderStatusId, int pageIndex, int pageSize);
        //test
        Task<Order?> GetOrderByIdAsync(int id);
        Task UpdateOrderAsync(Order order);
        //test
        //Task<Pagination<OrderWithDetailViewModel>> GetByOrderStatusPageAsync(int statusId, int pageIndex, int pageSize);
    }
}
