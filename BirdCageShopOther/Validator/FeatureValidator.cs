using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Feature;
using BirdCageShopViewModel.ProductFeature;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopOther.Validator
{
    public class FeatureValidator : IFeatureValidator
    {
        private readonly  FeatureAddRule validationRules;
        public FeatureValidator(FeatureAddRule featureAddValidator)
        {
            validationRules = featureAddValidator;
        }

        public IValidator<CreateFeature> CreateFeatureValidator => validationRules;
    }
}
