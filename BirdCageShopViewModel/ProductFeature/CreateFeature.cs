﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.ProductFeature
{
    public class CreateFeature
    {
        public string FeatureName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public decimal? Price { get; set; }
        public bool IsDelete { get; set; }

    }
    public class FeatureAddRule : AbstractValidator<CreateFeature>
    {
        public FeatureAddRule()
        {
            RuleFor(x => x.FeatureName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than or equals 50 characters."); ;
            RuleFor(o => o.CreatedAt)
               .Must(IsValidDate).WithMessage("Invalid {PropertyName}, The time gap must be around 10 year from the present and not exceeding 1 years");
            RuleFor(o => o.Price)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotEmpty().WithMessage("{PropertyName} is empty")
               .GreaterThan(0).WithMessage("Out of range!!")
               .LessThan(100000000).WithMessage("Out of range!!");
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