using BirdCageShopDbContext;
using BirdCageShopInterface;
using BirdCageShopInterface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory
{
    public class UnitOfWork  : IUnitOfWork
    {
        private readonly BirdCageShopContext _context;
        private IRoleRepository _roleRepository;

        public UnitOfWork(BirdCageShopContext context, IRoleRepository roleRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
        }

        public IRoleRepository RoleRepository => _roleRepository;

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
