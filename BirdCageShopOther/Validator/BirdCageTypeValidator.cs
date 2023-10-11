using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.BirdCageType;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopOther.Validator
{
    public class BirdCageTypeValidator : IBirdCageTypeValidator
    {
        private readonly BirdCageTypeAddRule _birdCageTypeAddRule;

        public BirdCageTypeValidator(BirdCageTypeAddRule birdCageTypeAddRule)
        {
            _birdCageTypeAddRule = birdCageTypeAddRule;
        }
        public IValidator<CreateBirdCageType> BirdCageTypeAddValidator => _birdCageTypeAddRule;
    }
}
