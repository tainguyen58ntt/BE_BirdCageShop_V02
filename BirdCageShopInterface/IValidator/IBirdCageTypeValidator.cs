﻿using BirdCageShopViewModel.BirdCageType;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IValidator
{
    public interface IBirdCageTypeValidator
    {
        IValidator<CreateBirdCageType> BirdCageTypeAddValidator { get; }
    }
}