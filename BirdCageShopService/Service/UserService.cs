using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils.Pagination;
using BirdCageShopUtils.UtilMethod;
using BirdCageShopViewModel.Auth;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserValidator _userValidator;

        public UserService(IUserValidator userValidator, IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
            _userValidator = userValidator;
        }

        //public UserService(ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IUserValidator userValidator, IConfiguration configuration) : base(timeService,unitOfWork, mapper,configuration)
        //{
        //    //_roleValidator = roleValidator;
        //    _userValidator = userValidator;
        //}

        //public async Task<string?> AuthorizeAsync(SignInViewModel vm)
        //{
        //    var user = await _unitOfWork.UserRepository.AuthorizeAsync(vm.Email, vm.Password);
        //    if (user is null) return null;
        //    //if (_configuration == null) return null;
        //    //if (_timeService == null) return null;
        //    //if (user.Role.RoleName == null) return null;
        //    var roleName = await _unitOfWork.UserRepository.GetRoleNameByUserIdAsync(user.Id);
        //    var accessToken = user.GenerateToken(user.Id, _configuration, _timeService.GetCurrentTimeInVietnam(), 60 * 24 * 30, roleName);


        //    return accessToken;
        //}

        //public async Task<bool> ChangePasswordAsync(UserChangePasswordViewModel vm)
        //{
        //    //var currentUserId = _claimService.GetCurrentUserId();
        //    //if (currentUserId == -1) return false;
        //    //test
        //    int currentUserId = 5;
        //    //test

        //    var user = await _unitOfWork.UserRepository.GetByIdAsync(currentUserId);
        //    if (user == null) return false;
        //    user.PasswordHash = vm.NewPassword.BCryptSaltAndHash();
        //    //
        //    _unitOfWork.UserRepository.Update(user);
        //    return await _unitOfWork.SaveChangesAsync();
        //}

        //public async Task<bool> DeleteAsync(User user)
        //{
        //    user.IsDelete = true;
        //    _unitOfWork.UserRepository.Update(user);
        //    return await _unitOfWork.SaveChangesAsync();
        //}




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

        //public async Task<User?> GetUserByIdAsync(int id)
        //{
        //    var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        //    return user;
        //}

        //public async Task<bool> IsExistsEmailAsync(string email)
        //{
        //    var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);

        //    return user != null;

        //}

        //public async Task<bool> RegisterAsync(UserSignUpViewModel vm)
        //{
        //    var user = _mapper.Map<User>(vm);
        //    user.PasswordHash = vm.Password.BCryptSaltAndHash();
        //    var role = await _unitOfWork.RoleRepository.GetByNameAsync("Customer");
        //    if(role != null) { 
        //        user.RoleId  = role.Id;
        //    }

        //    await _unitOfWork.UserRepository.AddAsync(user);
        //    return await _unitOfWork.SaveChangesAsync();


        //}

        //public Task<ValidationResult> ValidateChangePasswordAsync(UserChangePasswordViewModel vm)
        //{
        //    return _userValidator.UserChangePasswordValidator.ValidateAsync(vm);
        //}

        //public Task<ValidationResult> ValidateUserSignUpAsync(UserSignUpViewModel vm)
        //{
        //    return _userValidator.UserSignUpValidator.ValidateAsync(vm);
        //}
    }
}
