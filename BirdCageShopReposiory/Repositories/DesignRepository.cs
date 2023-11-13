using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class DesignRepository : BaseRepository<Design>, IDesignRepository
    {
        public DesignRepository(BirdCageShopContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Design>> GetDesignByUserIdAsync(string userId)
        {
           
            return await _context.Set<Design>()
                .Where(f => f.ApplicationUserId == userId)
                .ToListAsync();

            
        }
    }
}
