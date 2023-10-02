﻿using BirdCageShopViewModel.Role;
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
    }
}
