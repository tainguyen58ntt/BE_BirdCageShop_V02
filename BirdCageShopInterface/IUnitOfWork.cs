using BirdCageShopInterface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
