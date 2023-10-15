using BirdCageShopViewModel.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.Order
{
    public class ShippingDetailAddViewModel
    {
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string? VourcherCode { get; set; }
    }

    public class ShippingDetailAddRule : AbstractValidator<ShippingDetailAddViewModel>
    {
        public ShippingDetailAddRule()
        {
            RuleFor(c => c.Phone)
                .NotEmpty()
                .WithMessage("Phone can not be empty");
            RuleFor(c => c.StreetAddress)
              .NotEmpty()
              .WithMessage("StreetAddress can not be empty");
            RuleFor(c => c.City)
              .NotEmpty()
              .WithMessage("City can not be empty");

        }
    }
}
