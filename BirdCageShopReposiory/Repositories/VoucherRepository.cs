using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(BirdCageShopContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Voucher>> GetAllAsync()
        {
            return await _context.Set<Voucher>()
            .AsNoTracking()
            .Where(x => x.ExpirationDate.Value > DateTime.Now)
            .ToListAsync();
        }
    }
}
