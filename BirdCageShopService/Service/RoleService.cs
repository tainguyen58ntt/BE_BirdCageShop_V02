using AutoMapper;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Role;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IRoleValidator _roleValidator;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, IRoleValidator roleValidator) : base(unitOfWork,mapper){
            _roleValidator = roleValidator; 
        }
        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();
            return _mapper.Map<List<RoleViewModel>>(roles);
        }

        public Task<ValidationResult> ValidateRoleAddAsync(RoleAddViewModel vm)
        {
            return _roleValidator.RoleAddValidator.ValidateAsync(vm);
        }
    }
}
