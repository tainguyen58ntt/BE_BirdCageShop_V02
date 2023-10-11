using BirdCageShopDbContext;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BirdCageShopContext _context;
        private IRoleRepository _roleRepository;
        private ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository;
        private IVoucherRepository _voucherRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
		private IShoppingCartRepository _shoppingCartRepository;
        private IWishlistRepository _wishlistRepository;
        private IFeatureRepository _featureRepository;
        public UnitOfWork(BirdCageShopContext context, IRoleRepository roleRepository, IUserRepository userRepository, IVoucherRepository voucherRepository,
            IWishlistRepository wishlistRepository, IOrderRepository orderRepository,
            ICategoryRepository categoryRepository, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository,
            IFeatureRepository featureRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _voucherRepository = voucherRepository;
            _categoryRepository = categoryRepository;  
            _orderRepository = orderRepository; 
            _productRepository = productRepository; 
            _shoppingCartRepository = shoppingCartRepository;   
            _wishlistRepository = wishlistRepository;   
            _featureRepository = featureRepository;
            
        }

        public IRoleRepository RoleRepository => _roleRepository;

        public IUserRepository UserRepository => _userRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductRepository ProductRepository => _productRepository;

        public IVoucherRepository VoucherRepository => _voucherRepository;

        public IOrderRepository OrderRepository => _orderRepository;
		public IShoppingCartRepository ShoppingCartRepository => _shoppingCartRepository;
        public IWishlistRepository WishlistRepository => _wishlistRepository;
        public IFeatureRepository FeatureRepository => _featureRepository;
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
