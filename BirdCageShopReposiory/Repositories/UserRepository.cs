using BirdCageShopDbContext;
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
    }
}
