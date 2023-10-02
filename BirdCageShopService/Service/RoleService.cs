using AutoMapper;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class RoleService : BaseService, IRoleService
    {

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork,mapper){

        }
        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();
            return _mapper.Map<List<RoleViewModel>>(roles);
        }
    }
}
