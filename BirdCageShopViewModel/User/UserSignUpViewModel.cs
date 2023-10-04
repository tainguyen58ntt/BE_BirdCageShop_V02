using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.User
{
    public class UserSignUpViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserSignUpRule : AbstractValidator<UserSignUpViewModel>
    {
        public UserSignUpRule()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("User first name must not be empty");




            RuleFor(x => x.LastName)
              .NotEmpty()
              .WithMessage("User last name must not be empty");
            

            RuleFor(x => x.Email)
               .NotNull()
               .NotEmpty()
               .EmailAddress()
               .WithMessage("Email is not valid format.");


            RuleFor(x => x.Password)
            .Length(6, 20)
            .WithMessage("Password must between 6..20 characters");
        }
    }


}
