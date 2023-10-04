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
        private IUserRepository _userRepository;
        private IVoucherRepository _voucherRepository;

        public UnitOfWork(BirdCageShopContext context, IRoleRepository roleRepository, IUserRepository userRepository, IVoucherRepository voucherRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _voucherRepository = voucherRepository;
        }

        public IRoleRepository RoleRepository => _roleRepository;

        public IUserRepository UserRepository => _userRepository;

        public IVoucherRepository VoucherRepository => _voucherRepository;

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
