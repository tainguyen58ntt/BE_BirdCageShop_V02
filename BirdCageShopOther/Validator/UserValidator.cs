using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopOther.Validator
{
    public class UserValidator : IUserValidator
    {
        private readonly UserSignUpRule _userSignUpValidator;
        private readonly UserChangePasswordRule _userChangePassswordValidator;

        public UserValidator(UserSignUpRule userSignUpValidator, UserChangePasswordRule userChangePassswordValidator)
        {
            _userSignUpValidator = userSignUpValidator;
            _userChangePassswordValidator = userChangePassswordValidator;   
        }
        public IValidator<UserSignUpViewModel> UserSignUpValidator => _userSignUpValidator;

        public IValidator<UserChangePasswordViewModel> UserChangePasswordValidator => _userChangePassswordValidator;
    }
}
