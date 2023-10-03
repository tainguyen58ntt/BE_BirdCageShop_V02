using BirdCageShopUtils.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IRepositories
{
    public interface IBaseRepository<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel?> GetByIdAsync(int id);
        Task<Pagination<TModel>> GetPaginationAsync(int pageIndex, int pageSize);
        Task AddAsync(TModel entity);
        void Update(TModel entity);
        void Delete(TModel entity);
    }
}
