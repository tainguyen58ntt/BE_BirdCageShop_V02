using AutoMapper;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class OrderService:BaseService, IOrderService 
    {
		public OrderService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
		{
		}

        public async Task<OrderWithDetailViewModel?> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderWithDetailViewModel>(result);
        }

        //public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IUserValidator userValidator, IConfiguration configuration, ITimeService timeService) : base(timeService,unitOfWork, mapper, configuration)
        //{

        //      }

        public async Task<Pagination<OrderWithDetailViewModel>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.OrderRepository.GetPaginationAsync(pageIndex, pageSize);
            return _mapper.Map<Pagination<OrderWithDetailViewModel>>(result);
        }
    }
}
