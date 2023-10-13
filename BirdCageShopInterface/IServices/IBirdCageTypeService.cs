using BirdCageShopViewModel.BirdCageType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IBirdCageTypeService : IBaseService
    {
        Task<List<GetBirdCageType>> GetAllAsync();
        Task<GetBirdCageType> GetAsync(int key);
        Task CreateBirdCageTypeAsync(CreateBirdCageType createBirdCageType);
        Task UpdateBirdCageTypeAsync(int key, UpdateBirdCageType updateBirdCageType);
        Task DeleteBirdCageTypeAsync(int key);
        Task<List<GetBirdCageType>> GetBirdCageTypeNameAsync(string BirdCageTypeName);
    }
}
