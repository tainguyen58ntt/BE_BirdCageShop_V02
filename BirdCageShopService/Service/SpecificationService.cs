using AutoMapper;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.Specification;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class SpecificationService : BaseService, ISpecificationService
    {
        /*private readonly ISpecificationValidator _featureValidator;*/

        public SpecificationService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {

        }

        public async Task CreateSpecificationAsync(CreateSpecifications requestBody)
        {
            Specification specification = _mapper.Map<Specification>(requestBody);
            if (specification == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            await _unitOfWork.SpecificationRepository.AddAsync(specification);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteSpecificationAsync(int key)
        {
            Specification? specification = await _unitOfWork.SpecificationRepository.GetByIdAsync(key);
            if (specification == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            specification.IsDelete = false;
            _unitOfWork.SpecificationRepository.Update(specification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<GetSpecifications>> GetAllAsync()
        {
            try
            {
                List<GetSpecifications> specifications = _mapper.Map<List<GetSpecifications>>(
                    (await _unitOfWork.SpecificationRepository.GetAllAsync())
                    .Where(obj => obj.IsDelete == true)
                );

                return specifications;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetSpecifications> GetAsync(int key)
        {
            Specification specification = await _unitOfWork.SpecificationRepository.GetByIdAsync(key);
            if (specification == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            var result = _mapper.Map<GetSpecifications>(specification);
            return result;
        }

        public async Task<List<GetSpecifications>> GetSpecificationsNameAsync(string SpecificationName)
        {
            try
            {
                List<GetSpecifications> specifications = _mapper.Map<List<GetSpecifications>>(
                    (await _unitOfWork.SpecificationRepository.GetAllAsync())
                    .Where(obj => obj.SpecificationName.Contains(SpecificationName, StringComparison.OrdinalIgnoreCase) && obj.IsDelete == true)
                );

                return specifications;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task UpdateSpecificationAsync(int key, UpdateSpecifications requestBody)
        {
            try
            {
                if (requestBody.Id != key)
                {
                    throw new Exception("Invalid Id");
                }
                Specification specification = await _unitOfWork.SpecificationRepository.GetByIdAsync(key);

                if (specification == null)
                {
                    throw new Exception(" Specification does not exist in the system.");
                }


                if (!string.IsNullOrEmpty(requestBody.SpecificationName))
                {
                    specification.SpecificationName = requestBody.SpecificationName;
                }
                if (requestBody.Price >= 0 && requestBody.Price <= 10000000)
                {
                    specification.Price = requestBody.Price;
                }

                if (!string.IsNullOrEmpty(requestBody.SpecificationValue))
                {
                    specification.SpecificationValue = requestBody.SpecificationValue;
                }

                specification.ModiedAt = DateTime.Now;


                _unitOfWork.SpecificationRepository.Update(specification);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /* public Task<ValidationResult> ValidateProductAsync(CreateBirdCageType vm)
         {
             return _featureValidator.BirdCageTypeAddValidator.ValidateAsync(vm);
         }*/
    }
}
