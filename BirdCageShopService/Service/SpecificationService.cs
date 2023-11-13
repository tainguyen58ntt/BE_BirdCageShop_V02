using AutoMapper;
using BirdCageShopDomain.Models;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface;
using BirdCageShopViewModel.Feature;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdCageShopViewModel.Specification;
using BirdCageShopDbContext.Models;

namespace BirdCageShopService.Service
{
    public class SpecificationService : BaseService, ISpecificationService
    {

        public SpecificationService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {

        }

        public async Task CreateSpecificationAsync(CreateSpecification requestBody)
        {
            Specification feature = _mapper.Map<Specification>(requestBody);
            if (feature == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            feature.IsDelete = false;
            feature.CreatedAt = DateTime.Now;
            await _unitOfWork.SpecificationRepository.AddAsync(feature);
            await _unitOfWork.SaveChangesAsync();

        }



        public async Task DeleteSpecificationAsync(int key)
        {
            Specification? feature = await _unitOfWork.SpecificationRepository.GetByIdAsync(key);
            if (feature == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            feature.IsDelete = true;
            _unitOfWork.SpecificationRepository.Update(feature);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<GetSpecification>> GetAllAsync()
        {
            try
            {
                List<GetSpecification> features = _mapper.Map<List<GetSpecification>>(
                    (await _unitOfWork.SpecificationRepository.GetAllAsync())
                    .Where(obj => obj.IsDelete == false)
                );

                return features;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetSpecification> GetAsync(int key)
        {
            Specification feature = await _unitOfWork.SpecificationRepository.GetByIdAsync(key);
            if (feature == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            var result = _mapper.Map<GetSpecification>(feature);
            return result;
        }

        public async Task<List<GetSpecification>> GetSpecificationNameAsync(string featureName)
        {
            try
            {
                List<GetSpecification> features = _mapper.Map<List<GetSpecification>>(
                    (await _unitOfWork.SpecificationRepository.GetAllAsync())
                    .Where(obj => obj.SpecificationName.Contains(featureName, StringComparison.OrdinalIgnoreCase) && obj.IsDelete == true)
                );

                return features;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task UpdateSpecificationAsync(int key, UpdateSpecification requestBody)
        {
            try
            {

                Specification feature = await _unitOfWork.SpecificationRepository.GetByIdAsync(key);

                if (feature == null)
                {
                    throw new Exception(" Feature does not exist in the system.");
                }


                if (!string.IsNullOrEmpty(requestBody.SpecificationName))
                {
                    feature.SpecificationName = requestBody.SpecificationName;
                }
                if (!string.IsNullOrEmpty(requestBody.SpecificationValue))
                {
                    feature.SpecificationValue = requestBody.SpecificationValue;
                }

                feature.ModiedAt = DateTime.Now;


                _unitOfWork.SpecificationRepository.Update(feature);
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
