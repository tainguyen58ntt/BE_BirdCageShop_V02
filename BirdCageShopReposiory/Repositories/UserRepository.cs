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

namespace BirdCageShopReposiory.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(BirdCageShopContext context) : base(context)
        {
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
    }
}
