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
        //private IRoleRepository _roleRepository;
        private ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository;
        private IVoucherRepository _voucherRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IShoppingCartRepository _shoppingCartRepository;
        private IWishlistRepository _wishlistRepository;
        private IStatusRepository _statusRepository;
        private IOrderDetailRepository _orderDetailRepository;
        //public UnitOfWork(BirdCageShopContext context, IVoucherRepository voucherRepository,
        //    IWishlistRepository wishlistRepository, IOrderRepository orderRepository,
        //    ICategoryRepository categoryRepository, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository, IStatusRepository statusRepository
        //    , IOrderDetailRepository orderDetailRepository)
        //{
        //    _context = context;
        //    //_roleRepository = roleRepository;
        //    //_userRepository = userRepository;
        //    _voucherRepository = voucherRepository;
        //    _categoryRepository = categoryRepository;
        //    _orderRepository = orderRepository;
        //    _productRepository = productRepository;
        //    _shoppingCartRepository = shoppingCartRepository;
        //    _wishlistRepository = wishlistRepository;
        //    _statusRepository = statusRepository;
        //    _orderDetailRepository = orderDetailRepository;


        //}


        public UnitOfWork(BirdCageShopContext context, IVoucherRepository voucherRepository,
            IWishlistRepository wishlistRepository, IOrderRepository orderRepository,
            ICategoryRepository categoryRepository, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository, IStatusRepository statusRepository
            , IOrderDetailRepository orderDetailRepository, IUserRepository userRepository)
        {
            _context = context;
            //_roleRepository = roleRepository;
            _userRepository = userRepository;
            _voucherRepository = voucherRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _wishlistRepository = wishlistRepository;
            _statusRepository = statusRepository;
            _orderDetailRepository = orderDetailRepository;


        }

        //public IRoleRepository RoleRepository => _roleRepository;
        public IStatusRepository StatusRepository => _statusRepository;

        public IUserRepository UserRepository => _userRepository;
        public IOrderDetailRepository OrderDetailRepository => _orderDetailRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductRepository ProductRepository => _productRepository;

        public IVoucherRepository VoucherRepository => _voucherRepository;

        public IOrderRepository OrderRepository => _orderRepository;
        public IShoppingCartRepository ShoppingCartRepository => _shoppingCartRepository;
        public IWishlistRepository WishlistRepository => _wishlistRepository;

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
