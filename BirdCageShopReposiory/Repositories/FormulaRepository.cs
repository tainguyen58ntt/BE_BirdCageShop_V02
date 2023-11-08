using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
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
    public class FormulaRepository : BaseRepository<Formula>, IFormulaRepository
    {
        public FormulaRepository(BirdCageShopContext context) : base(context)
        {
        }

        public virtual async Task<Formula?> GetByIdAsync(int id)
        {
            return await _context.Set<Formula>()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

        }
        public async Task<IEnumerable<Formula>> GetFormulaByBirdCageTypeIdAsync(int birdCageTypeId)
        {
            return await _context.Set<Formula>()
                .Where(f => f.BirdCageTypeId == birdCageTypeId)
                 .Include(p => p.FormulaSpecifications)
                .ThenInclude(ps => ps.Specification)
                .ToListAsync();

        }
        public override async Task<IEnumerable<Formula>> GetAllAsync()
        {
            return await _context.Set<Formula>()
                .AsNoTracking()
                .Include(p => p.FormulaSpecifications)
                .ThenInclude(ps => ps.Specification)
                .ToListAsync();
        }
        public override async Task<Pagination<Formula>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var totalCount = await _context.Set<Formula>().CountAsync();
            var items = await _context.Set<Formula>()
                .AsNoTracking()

                .Include(p => p.FormulaSpecifications)
                .ThenInclude(ps => ps.Specification)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<Formula>()
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
