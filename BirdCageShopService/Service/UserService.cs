using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
using FluentValidation.Results;
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

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IUserValidator userValidator) : base(unitOfWork, mapper)
        {
            //_roleValidator = roleValidator;
            _userValidator = userValidator;
        }

        public async Task<bool> DeleteAsync(User user)
        {
            user.IsDelete = true;
            _unitOfWork.UserRepository.Update(user);
            return await _unitOfWork.SaveChangesAsync();
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

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return user;
        }

        public async Task<bool> IsExistsEmailAsync(string email)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email); 

            return user != null;    

        }

        public async Task<bool> RegisterAsync(UserSignUpViewModel vm)
        {
            //   var user = _mapper.Map<User>(vm);
            //user.PasswordHash  = vm.Password.BCryptSaltAndHash
            return true;

        }

        public Task<ValidationResult> ValidateUserSignUpAsync(UserSignUpViewModel vm)
        {
            return _userValidator.UserSignUpValidator.ValidateAsync(vm);
        }
    }
}
