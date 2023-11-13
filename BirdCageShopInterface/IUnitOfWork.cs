using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface
{
    public interface IUnitOfWork
    {
        //IRoleRepository RoleRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }
        IVoucherRepository VoucherRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IWishlistRepository WishlistRepository { get; }
        IStatusRepository StatusRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IFormulaRepository FormulaRepository { get; }
        IBirdCageTypeRepository BirdCageTypeRepository { get; }
        ISpecificationRepository SpecificationRepository { get; }
        IFormulaSpecificationRepository FormulaSpecificationRepository { get; }
        IFeatureRepository FeatureRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IProductFeatureRepository ProductFeatureRepository { get; }
        IProductSpecificationsRepository ProductSpecificationsRepository { get; }
        IDesignRepository DesignRepository { get; }
        Task<bool> SaveChangesAsync();
        public IDbContextTransaction Transaction();
    }
}
