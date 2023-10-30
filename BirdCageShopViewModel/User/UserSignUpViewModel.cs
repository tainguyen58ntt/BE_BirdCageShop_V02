using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.User
{
    public class UserSignUpViewModel
    {
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class UserSignUpRule : AbstractValidator<UserSignUpViewModel>
    {
        private bool IsValidUserName(string userName)
        {
            // Use regular expressions to check if the username contains only letters and digits.
            return !string.IsNullOrWhiteSpace(userName) && Regex.IsMatch(userName, "^[a-zA-Z0-9]+$");
        }
        public UserSignUpRule()
        {

            RuleFor(x => x.UserName)
             .NotEmpty()
             .WithMessage("Username must not be empty")
             .Must(IsValidUserName)
             .WithMessage("Username can only contain letters and digits");





            RuleFor(x => x.Email)
               .NotNull()
               .NotEmpty()
               .EmailAddress()
               .WithMessage("Email is not valid format.");


            RuleFor(x => x.Password)
              .NotEmpty()
              .WithMessage("Password must not be empty")
              .Length(6, 20)
              .WithMessage("Password must be between 6 and 20 characters")
              .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$")
              .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character.");
        }
    }


}
