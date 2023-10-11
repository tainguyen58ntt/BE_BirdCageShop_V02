using AutoMapper;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.BirdCageType;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class BirdCageTypeService : BaseService, IBirdCageTypeService
    {
        /*private readonly IBirdCageTypeValidator _featureValidator;*/

        public BirdCageTypeService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {

        }

        public async Task CreateBirdCageTypeAsync(CreateBirdCageType requestBody)
        {
            BirdCageType birdCageType = _mapper.Map<BirdCageType>(requestBody);
            if (birdCageType == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            await _unitOfWork.BirdCageTypeRepository.AddAsync(birdCageType);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteBirdCageTypeAsync(int key)
        {
            BirdCageType? birdCageType = await _unitOfWork.BirdCageTypeRepository.GetByIdAsync(key);
            if (birdCageType == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            birdCageType.IsDelete = false;
            _unitOfWork.BirdCageTypeRepository.Update(birdCageType);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<GetBirdCageType>> GetAllAsync()
        {
            try
            {
                List<GetBirdCageType> birdCageTypes = _mapper.Map<List<GetBirdCageType>>(
                    (await _unitOfWork.BirdCageTypeRepository.GetAllAsync())
                    .Where(obj => obj.IsDelete == true)
                );

                return birdCageTypes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetBirdCageType> GetAsync(int key)
        {
            BirdCageType birdCageType = await _unitOfWork.BirdCageTypeRepository.GetByIdAsync(key);
            if (birdCageType == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            var result = _mapper.Map<GetBirdCageType>(birdCageType);
            return result;
        }

        public async Task<List<GetBirdCageType>> GetBirdCageTypeNameAsync(string birdCageTypeName)
        {
            try
            {
                List<GetBirdCageType> birdCageTypes = _mapper.Map<List<GetBirdCageType>>(
                    (await _unitOfWork.BirdCageTypeRepository.GetAllAsync())
                    .Where(obj => obj.TypeName.Contains(birdCageTypeName, StringComparison.OrdinalIgnoreCase))
                );

                return birdCageTypes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateBirdCageTypeAsync(int key, UpdateBirdCageType requestBody)
        {
            try
            {
                if (requestBody.Id != key)
                {
                    throw new Exception("Invalid Id");
                }
                BirdCageType birdCageType = await _unitOfWork.BirdCageTypeRepository.GetByIdAsync(key);

                if (birdCageType == null)
                {
                    throw new Exception(" BirdCageType does not exist in the system.");
                }


                if (!string.IsNullOrEmpty(requestBody.TypeName))
                {
                    birdCageType.TypeName = requestBody.TypeName;
                }

                if (!string.IsNullOrEmpty(requestBody.Description))
                {
                    birdCageType.Description = requestBody.Description;
                }

                birdCageType.ModifiedAt = DateTime.Now;


                _unitOfWork.BirdCageTypeRepository.Update(birdCageType);
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