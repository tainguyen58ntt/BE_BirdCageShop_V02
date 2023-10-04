using BirdCageShopDbContext.Models;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
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

        Task<IEnumerable<UserViewModel>> GetUserAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> DeleteAsync(User user);
        Task<Pagination<UserViewModel>> GetPageAsync(int pageIndex, int pageSizes);
        Task<ValidationResult> ValidateUserSignUpAsync(UserSignUpViewModel vm);
        Task<bool> IsExistsEmailAsync(string email);
        Task<bool> RegisterAsync(UserSignUpViewModel vm);

        Task<ValidationResult> ValidateChangePasswordAsync(UserChangePasswordViewModel vm);
        Task<bool> ChangePasswordAsync(UserChangePasswordViewModel vm);
    }
}
