﻿using BirdCageShopDbContext.Models;
using birdcageshopinterface.IServices;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Auth;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IUserService : IBaseService
    {
        //Task<string?> AuthorizeAsync(SignInViewModel vm);

        ////
        Task<IEnumerable<UserViewModel>> GetUserAsync();

        //Task<Pagination<UserViewModel>> GetUserPageAsync(int pageIndex, int pageSize);

        Task<bool> IsProductPurchasedByCustomer(int productId);

        Task<Pagination<OrderWithDetailViewModel>> GetOrderHistoryAsync(int pageIndex, int pageSize);
        Task<Pagination<VourcherViewModel>> GetVoucherByUserIdAsync(int pageIndex, int pageSize);
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> GetUserIncludeUserDeletedByIdAsync(string id);
        Task<bool> DeleteAsync(ApplicationUser user);
        Task<bool> RecoverAsync(ApplicationUser user);
        //Task<Pagination<UserViewModel>> GetPageAsync(int pageIndex, int pageSizes);
        Task<ValidationResult> ValidateUserSignUpAsync(UserSignUpViewModel vm);
        //Task<bool> IsExistsEmailAsync(string email);
        //Task<bool> RegisterAsync(UserSignUpViewModel vm);

        Task<ValidationResult> ValidateChangePasswordAsync(UserChangePasswordViewModel vm);
        Task<ValidationResult> ValidateUpdateProfileAsync(UpdateProfileViewModel vm);
        Task<ValidationResult> ValidateResetPasswordAsync(ResetPassword vm);
        Task<bool> ChangePasswordAsync(UserChangePasswordViewModel vm);

        Task<bool> UpdateProfileAsync(UpdateProfileViewModel vm);


        Task<bool> isExistUsername(string username);
        Task<bool> isExistEmail(string email);
    }
}
