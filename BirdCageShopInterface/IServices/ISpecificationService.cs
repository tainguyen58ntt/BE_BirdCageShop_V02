using BirdCageShopViewModel.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface ISpecificationService : IBaseService
    {
        Task<List<GetSpecifications>> GetAllAsync();
        Task<GetSpecifications> GetAsync(int key);
        Task CreateSpecificationAsync(CreateSpecifications createSpecification);
        Task UpdateSpecificationAsync(int key, UpdateSpecifications updateSpecification);
        Task DeleteSpecificationAsync(int key);
        Task<List<GetSpecifications>> GetSpecificationsNameAsync(string specificationName);
        /*Task<ValidationResult> ValidateProductAsync(CreateFeature createFeature);*/
    }
}
