﻿using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IValidator
{
    public interface IUserValidator
    {
        IValidator<UserSignUpViewModel> UserSignUpValidator { get; }
    }
}
