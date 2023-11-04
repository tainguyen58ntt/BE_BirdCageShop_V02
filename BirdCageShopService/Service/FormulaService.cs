using AutoMapper;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopViewModel.Formula;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class FormulaService : BaseService, IFormulaService
    {
        public FormulaService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
        }

        public async Task CreateFormulaAsync(CreateFormulaViewModel createFormulaViewModel)
        {
            try
            {

                BirdCageType existedBirdCageType = (await _unitOfWork.BirdCageTypeRepository.GetByIdAsync(createFormulaViewModel.BirdCageTypeId));
                if (existedBirdCageType == null)
                {
                    throw new Exception("BirdCageType Id does not exist in the system.");
                }
                Formula formula = new Formula()
                {
                    Code = createFormulaViewModel.Code,
                    MinWidth = createFormulaViewModel.MinWidth,
                    Material = createFormulaViewModel.Material,
                    MaxBars = createFormulaViewModel.MaxBars,
                    MaxHeight = createFormulaViewModel.MaxHeight,
                    MinHeight = createFormulaViewModel.MinHeight,
                    MaxWidth = createFormulaViewModel.MaxWidth,
                    MinBars = createFormulaViewModel.MinBars,
                    Price = createFormulaViewModel.Price,
                    BirdCageTypeId = createFormulaViewModel.BirdCageTypeId,
                    ConstructionTime = createFormulaViewModel.ConstructionTime,
                };

                await _unitOfWork.FormulaRepository.AddAsync(formula);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FormulaViewModel>> GetAllAsync()
        {
            try
            {
                IEnumerable<Formula> formulas = await _unitOfWork.FormulaRepository.GetAllAsync();
                List<FormulaViewModel> formulaViewModels = _mapper.Map<List<FormulaViewModel>>(
                     await _unitOfWork.FormulaRepository.GetAllAsync()
                );

                return formulaViewModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FormulaViewModel>> GetByIdAsync(int key)
        {
            try
            {
                BirdCageType birdCageType = await _unitOfWork.BirdCageTypeRepository.GetByIdAsync(key);

                if (birdCageType == null)
                {
                    throw new Exception("BirdCageType ID does not exist in the system.");
                }
                List<FormulaViewModel> formulaViewModels = _mapper.Map<List<FormulaViewModel>>(
                  await _unitOfWork.FormulaRepository.GetFormulaByBirdCageTypeIdAsync(key)
              );
                return formulaViewModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateFormulaAsync(int key, UpdateFormulaViewModel updateFormulaViewModel)
        {
            try
            {
                Formula existedFormula = await _unitOfWork.FormulaRepository.GetByIdAsync(key);

                if (existedFormula == null)
                {
                    throw new Exception("Formula Id does not exist in the system.");
                }
                BirdCageType existedBirdCageType = (await _unitOfWork.BirdCageTypeRepository.GetByIdAsync(updateFormulaViewModel.BirdCageTypeId));
                if (existedBirdCageType == null)
                {
                    throw new Exception("BirdCageType Id does not exist in the system.");
                }
                else
                {
                    existedFormula.BirdCageTypeId = updateFormulaViewModel.BirdCageTypeId;
                }
                if (!string.IsNullOrEmpty(updateFormulaViewModel.Code))
                {
                    existedFormula.Code = updateFormulaViewModel.Code;
                }
                if (!string.IsNullOrEmpty(updateFormulaViewModel.Material))
                {
                    existedFormula.Material = updateFormulaViewModel.Material;
                }
                if (updateFormulaViewModel.MinWidth != null)
                {
                    existedFormula.MinWidth = (int)updateFormulaViewModel.MinWidth;
                }
                if (updateFormulaViewModel.MaxWidth != null)
                {
                    existedFormula.MaxWidth = (int)updateFormulaViewModel.MaxWidth;
                }
                if (updateFormulaViewModel.MinHeight != null)
                {
                    existedFormula.MinHeight = (int)updateFormulaViewModel.MinHeight;
                }
                if (updateFormulaViewModel.MaxHeight != null)
                {
                    existedFormula.MaxHeight = (int)updateFormulaViewModel.MaxHeight;
                }
                if (updateFormulaViewModel.MinBars != null)
                {
                    existedFormula.MinBars = (int)updateFormulaViewModel.MinBars;
                }
                if (updateFormulaViewModel.MaxBars != null)
                {
                    existedFormula.MaxBars = (int)updateFormulaViewModel.MaxBars;
                }
                if (updateFormulaViewModel.Price != null)
                {
                    existedFormula.Price = (decimal)updateFormulaViewModel.Price;
                }
                if (updateFormulaViewModel.ConstructionTime != null)
                {
                    existedFormula.ConstructionTime = (int)updateFormulaViewModel.ConstructionTime;
                }

                _unitOfWork.FormulaRepository.Update(existedFormula);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
