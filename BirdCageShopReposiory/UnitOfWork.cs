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
		public UnitOfWork(BirdCageShopContext context, IRoleRepository roleRepository, IUserRepository userRepository, IVoucherRepository voucherRepository,
            IOrderRepository orderRepository,
            ICategoryRepository categoryRepository, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _voucherRepository = voucherRepository;
            _categoryRepository = categoryRepository;  
            _orderRepository = orderRepository; 
            _productRepository = productRepository; 
            _shoppingCartRepository = shoppingCartRepository;   
            
        }

        public IRoleRepository RoleRepository => _roleRepository;

        public IUserRepository UserRepository => _userRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductRepository ProductRepository => _productRepository;

        public IVoucherRepository VoucherRepository => _voucherRepository;

        public IOrderRepository OrderRepository => _orderRepository;
		public IShoppingCartRepository ShoppingCartRepository => _shoppingCartRepository;

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
