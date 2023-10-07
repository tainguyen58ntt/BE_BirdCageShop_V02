using BirdCageShopDbContext;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using BirdCageShopUtils.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdCageShopUtils.UtilMethod;

namespace BirdCageShopReposiory.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BirdCageShopContext context) : base(context)
        {
        }

		public async Task<User?> AuthorizeAsync(string email, string password)
		{
			var admin = await _context.Set<User>()
			  .AsNoTracking()
              .Include(u => u.Role)
			  .FirstOrDefaultAsync(x => x.Email == email && !x.IsDelete);
			if (admin == null) return null;
			if (password.IsCorrectHashSource(admin.PasswordHash)) return admin;
			return null;
		}

		public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Set<User>()
                .AsNoTracking()
                .Where(x => !x.IsDelete)
                .ToListAsync();
        }

        public override async Task<Pagination<User>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var source = _context.Set<User>()
               .Where(x => !x.IsDelete);
            //
            var totalCount = await source.CountAsync();
            var items = await source
                .AsNoTracking()
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<User>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            return result;
        }

		public async Task<string?> GetRoleNameByUserIdAsync(int id)
		{
			var user = await _context.Set<User>().Include(u => u.Role).FirstOrDefaultAsync(x => x.Id == id && x.IsDelete == false);
            var rs = user.Role.RoleName;
			return rs;
		}

		public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(x => x.Email.Equals(email) && x.IsDelete == false);
			
			return user;
        }
    }
}
