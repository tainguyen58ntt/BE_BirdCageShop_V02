using BirdCageShopDbContext;
using BirdCageShopDbContext.Models;
using BirdCageShopInterface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class RoleRepository: BaseRepository<Role>, IRoleRepository
    {

        public RoleRepository(BirdCageShopContext context) : base(context)
        {
        }

    }
}
