using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.User
{
    public class UserChangePasswordViewModel
    {
        public string OldPassword { get; set; }


        public string NewPassword { get; set; }


        public string ConfirmPassword { get; set; }
    }
    public class UserChangePasswordRule : AbstractValidator<UserChangePasswordViewModel>
    {
        public UserChangePasswordRule()
        {
            RuleFor(x => x.OldPassword)
            .NotEmpty()
            .WithMessage("Old password must not be empty")
             .Length(6, 20)
                .WithMessage("Old password must between 6..20 characters");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("New password must not be empty")
               .Length(6, 20)
                .WithMessage("New password must between 6..20 characters");


            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password must not be empty")
               .Length(6, 20)
                .WithMessage("New password must between 6..20 characters")
                .Equal(x => x.NewPassword)
                .WithMessage("Confirm password must match the new password");

        }
    }
}
