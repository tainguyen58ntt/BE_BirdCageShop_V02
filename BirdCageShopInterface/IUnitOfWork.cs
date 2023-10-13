﻿using BirdCageShopInterface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }
        IVoucherRepository VoucherRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
		IShoppingCartRepository ShoppingCartRepository { get; }
        IWishlistRepository WishlistRepository { get; }
        IFeatureRepository FeatureRepository { get; }
        IBirdCageTypeRepository BirdCageTypeRepository { get; }
        ISpecificationRepository SpecificationRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
