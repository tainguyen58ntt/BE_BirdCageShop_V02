using BirdCageShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
    public interface IDesignRepository : IBaseRepository<Design>
    {
        Task<IEnumerable<Design>> GetDesignByUserIdAsync(string userId);

    }
}
