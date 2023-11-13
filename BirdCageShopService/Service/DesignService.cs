using AutoMapper;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.BirdCageType;
using BirdCageShopViewModel.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class DesignService : BaseService, IDesignService
    {
        public DesignService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
        }

        public async Task CreateDesinAsync(CreateDesign requestBody)
        {
            Design design = _mapper.Map<Design>(requestBody);
            if (design == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            await _unitOfWork.DesignRepository.AddAsync(design);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteDesignAsync(string key)
        {
            List<Design> designViewModels = new List<Design>(
                     (await _unitOfWork.DesignRepository.GetAllAsync())
                     .Where(obj => obj.ApplicationUserId == key)
                  );
            foreach(Design design in designViewModels)
            {
                _unitOfWork.DesignRepository.Delete(design);
            }
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<List<DesignViewModel>> GetByIdAsync(string key)
        {
            try
            {
                List<DesignViewModel> designViewModels = _mapper.Map<List<DesignViewModel>>(
                    (await _unitOfWork.DesignRepository.GetAllAsync())
                    .Where(obj => obj.ApplicationUserId == key)
                 );
                return designViewModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
