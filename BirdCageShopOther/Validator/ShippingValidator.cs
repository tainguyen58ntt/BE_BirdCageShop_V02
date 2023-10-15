using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Role;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopOther.Validator
{
    public class ShippingDetailValidator : IShippingDetailValidator
    {
        private readonly ShippingDetailAddRule _shippingDetailAddValidator;

        public ShippingDetailValidator(ShippingDetailAddRule shippingDetailAddValidator)
        {
            _shippingDetailAddValidator = shippingDetailAddValidator;
        }
        public IValidator<ShippingDetailAddViewModel> ShippingDetailAddValidator => _shippingDetailAddValidator;
    }
}
