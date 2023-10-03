﻿using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IUserService: IBaseService
    {

        Task<IEnumerable<UserViewModel>> GetUserAsync();
        Task<Pagination<UserViewModel>> GetPageAsync(int pageIndex, int pageSizes);
    }
}