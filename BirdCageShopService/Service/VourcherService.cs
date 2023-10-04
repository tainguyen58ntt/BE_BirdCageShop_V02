using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class VourcherService : BaseService, IVourcherService
    {
        public VourcherService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        // get vourcher have expdate > datetime now
        public async Task<IEnumerable<VourcherViewModel>> GetVourcherAsync()
        {
            var vourchers = await _unitOfWork.VoucherRepository.GetAllAsync();
            return _mapper.Map<List<VourcherViewModel>>(vourchers);
        }
    }
}
