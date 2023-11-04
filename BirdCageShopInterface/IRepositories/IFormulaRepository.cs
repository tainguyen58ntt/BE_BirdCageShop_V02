using BirdCageShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
    public interface IFormulaRepository : IBaseRepository<Formula>
    {
        Task<IEnumerable<Formula>> GetFormulaByBirdCageTypeIdAsync(int birdCageTypeId);

    }
}
