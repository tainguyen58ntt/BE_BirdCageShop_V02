using BirdCageShopDbContext;
using BirdCageShopInterface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        protected readonly BirdCageShopContext _context;
        public BaseRepository(BirdCageShopContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TModel entity)
        {
            await _context.Set<TModel>().AddAsync(entity);
        }

        public void Delete(TModel entity)
        {
            _context.Set<TModel>().Remove(entity);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await _context.Set<TModel>().ToListAsync();
        }

        public async Task<TModel?> GetByIdAsync(int id)
        {
            return await _context.Set<TModel>().FindAsync(id);
        }

        public void Update(TModel entity)
        {
            _context.Set<TModel>().Update(entity);
        }
    }
}
