using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface.IRepositories;
using BirdCageShopUtils.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopReposiory.Repositories
{
    public class FormulaSpecificationRepository : BaseRepository<FormulaSpecification>, IFormulaSpecificationRepository
    {
        public FormulaSpecificationRepository(BirdCageShopContext context) : base(context)
        {
        }

       
    }
}
