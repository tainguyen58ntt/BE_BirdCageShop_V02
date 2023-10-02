using BirdCageShopViewModel.Role;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IRoleService: IBaseService
    {
        Task<IEnumerable<RoleViewModel>> GetRolesAsync();

        Task<ValidationResult> ValidateRoleAddAsync(RoleAddViewModel vm);
    }
}
