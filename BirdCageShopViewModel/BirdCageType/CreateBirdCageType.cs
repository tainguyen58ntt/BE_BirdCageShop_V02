using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.BirdCageType
{
    public class CreateBirdCageType
    {
        public string TypeName { get; set; } = null!;

        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }

        public bool IsDelete { get; set; }

    }
    public class BirdCageTypeAddRule : AbstractValidator<CreateBirdCageType>
    {
        public BirdCageTypeAddRule()
        {
            RuleFor(x => x.TypeName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than or equals 50 characters."); 
            RuleFor(o => o.CreateAt)
                .Must(IsValidDate).WithMessage("Invalid {PropertyName}, The time gap must be around 10 year from the present and not exceeding 1 years");
            RuleFor(x => x.TypeName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .MaximumLength(100).WithMessage("{PropertyName} must be less than or equals 100 characters.");
        }


        private bool IsValidDate(DateTime date)
        {
            int yearInput = date.Year;
            int yearNow = DateTime.UtcNow.Year;
            if (yearInput > (yearNow - 10) && yearInput < (yearNow + 1))
                return true;
            return false;
        }
    }
}
