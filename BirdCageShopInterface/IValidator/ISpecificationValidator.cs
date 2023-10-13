using BirdCageShopViewModel.ProductSpecifications;
using BirdCageShopViewModel.Specification;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IValidator
{
    public interface ISpecificationValidator
    {
        IValidator<CreateSpecifications> SpecificationsAddRule { get; }
    }
}
