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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {

        public OrderRepository(BirdCageShopContext context) : base(context)
        {
        }   
        public override async Task<Pagination<Order>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var source = _context.Set<Order>()
               .Include(o => o.Details).Include(o => o.User);
            //
            var totalCount = await source.CountAsync();
            var items = await source
                .AsNoTracking()
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<Order>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            return result;
        }

        public override async Task<Order?> GetByIdAsync(int id)
        {

            return await _context.Set<Order>()
                .AsNoTracking()
                .AsNoTrackingWithIdentityResolution()
                .Include(x => x.Details)
                .Include(x => x.User)
               .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
