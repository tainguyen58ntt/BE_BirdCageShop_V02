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

        public UserValidator(UserSignUpRule userSignUpValidator) {
            _userSignUpValidator = userSignUpValidator;
        }
        public IValidator<UserSignUpViewModel> UserSignUpValidator => _userSignUpValidator;
    }
}
