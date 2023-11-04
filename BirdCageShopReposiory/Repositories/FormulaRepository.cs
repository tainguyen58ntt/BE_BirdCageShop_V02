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
    public class FormulaRepository : BaseRepository<Formula>, IFormulaRepository
    {
        public FormulaRepository(BirdCageShopContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Formula>> GetFormulaByBirdCageTypeIdAsync(int birdCageTypeId)
        {
            return await _context.Set<Formula>()
                .Where(f => f.BirdCageTypeId == birdCageTypeId)
                .ToListAsync();

        }
    }
}
