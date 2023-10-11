using BirdCageShopViewModel.ProductFeature;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IFeatureService : IBaseService
    {
        Task<List<GetFeature>> GetAllAsync();
        Task<GetFeature> GetAsync(int key);
        Task CreateFeatureAsync(CreateFeature createFeature);
        Task UpdateFeatureAsync(int key, UpdateFeature updateFeature);
        Task DeleteFeatureAsync(int key);
        Task<List<GetFeature>> GetFeatureNameAsync(string featureName);
        /*Task<ValidationResult> ValidateProductAsync(CreateFeature createFeature);*/
    }
}
