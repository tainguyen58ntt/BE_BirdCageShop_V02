using AutoMapper;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class OrderService:BaseService, IOrderService 
    {

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            
        }

        public async Task<Pagination<OrderWithDetailViewModel>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.OrderRepository.GetPaginationAsync(pageIndex, pageSize);
            return _mapper.Map<Pagination<OrderWithDetailViewModel>>(result);
        }
    }
}
