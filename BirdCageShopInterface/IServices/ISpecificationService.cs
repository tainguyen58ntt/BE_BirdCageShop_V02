using BirdCageShopViewModel.Feature;
using BirdCageShopViewModel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface ISpecificationService
    {
        Task<List<GetSpecification>> GetAllAsync();
        Task<GetSpecification> GetAsync(int key);
        Task CreateSpecificationAsync(CreateSpecification createFeature);
        Task UpdateSpecificationAsync(int key, UpdateSpecification updateFeature);
        Task DeleteSpecificationAsync(int key);
        Task<List<GetSpecification>> GetSpecificationNameAsync(string featureName);
    }
}
