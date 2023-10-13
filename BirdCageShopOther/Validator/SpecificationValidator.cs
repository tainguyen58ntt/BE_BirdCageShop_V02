using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Specification;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopOther.Validator
{
    public class SpecificationValidator : ISpecificationValidator
    {
        private readonly SpecificationsAddRule validationRules;
    public SpecificationValidator(SpecificationsAddRule featureAddValidator)
    {
        validationRules = featureAddValidator;
    }

    public IValidator<CreateSpecifications> SpecificationsAddRule => validationRules;

    }
}
