using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.ProductFeature;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class FeatureService : BaseService, IFeatureService
    {
        private readonly IFeatureValidator _featureValidator;

        public FeatureService(IFeatureValidator featureValidator ,IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
            _featureValidator =  featureValidator;
        }

        public async Task CreateFeatureAsync(CreateFeature requestBody)
        {
             Feature feature = _mapper.Map<Feature>(requestBody);
            if (feature == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            await _unitOfWork.FeatureRepository.AddAsync(feature);
            await _unitOfWork.SaveChangesAsync();
            
        }

        public async Task DeleteFeatureAsync(int key)
        {
             Feature? feature = await _unitOfWork.FeatureRepository.GetByIdAsync(key);
            if (feature == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            feature.IsDelete = false;
            _unitOfWork.FeatureRepository.Update(feature);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<GetFeature>> GetAllAsync()
        {
            try
            {
                List<GetFeature> features = _mapper.Map<List<GetFeature>>(
                    (await _unitOfWork.FeatureRepository.GetAllAsync())
                    .Where(obj => obj.IsDelete == true)
                );

                return features;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetFeature> GetAsync(int key)
        {
            Feature feature = await _unitOfWork. FeatureRepository.GetByIdAsync(key);
            if (feature == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            var result = _mapper.Map<GetFeature>(feature);
            return result;
        }

        public async Task<List<GetFeature>> GetFeatureNameAsync(string featureName)
        {
            try
            {
                List<GetFeature> features = _mapper.Map<List<GetFeature>>(
                    (await _unitOfWork. FeatureRepository.GetAllAsync())
                    .Where(obj => obj.FeatureName.Contains(featureName, StringComparison.OrdinalIgnoreCase) && obj.IsDelete == true)
                );

                return features;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateFeatureAsync(int key, UpdateFeature requestBody)
        {
            try
            {
                if(requestBody.Id != key)
                {
                    throw new Exception("Invalid Id");
                }
                 Feature feature = await _unitOfWork. FeatureRepository.GetByIdAsync(key);

                if (feature == null)
                {
                    throw new Exception(" Feature does not exist in the system.");
                }


                if (!string.IsNullOrEmpty(requestBody.FeatureName))
                {
                     feature.FeatureName = requestBody.FeatureName;
                }

                if (requestBody.CreatedAt != null)
                {
                     feature.CreatedAt = requestBody.CreatedAt;
                }

                if (requestBody.Price >= 0 && requestBody.Price <=10000000)
                {
                     feature.Price = requestBody.Price;
                }

                feature.ModiedAt = DateTime.Now;


                _unitOfWork.FeatureRepository.Update(feature);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
                              
        }
       /* public Task<ValidationResult> ValidateProductAsync(CreateFeature vm)
        {
            return _featureValidator.CreateFeatureValidator.ValidateAsync(vm);
        }*/
    }
}
