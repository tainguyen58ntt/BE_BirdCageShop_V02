﻿using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class UserService : BaseService, IUserService
    {
        //private readonly IRoleValidator _roleValidator;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            //_roleValidator = roleValidator;
        }

        public async Task<Pagination<UserViewModel>> GetPageAsync(int pageIndex, int pageSizes)
        {
            var result = await _unitOfWork.UserRepository.GetPaginationAsync(pageIndex, pageSizes);
            return _mapper.Map<Pagination<UserViewModel>>(result);
        }

        public async Task<IEnumerable<UserViewModel>> GetUserAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }
    }
}