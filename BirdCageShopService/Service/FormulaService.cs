using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Feature;
using BirdCageShopViewModel.Formula;
using BirdCageShopViewModel.Product;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace BirdCageShopService.Service
{
    public class FormulaService : BaseService, IFormulaService
    {
        public FormulaService(IClaimService claimService, ITimeService timeService, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(claimService, timeService, unitOfWork, mapper, configuration)
        {
        }

        public async Task CreateFormulaAsync(CreateFormulaViewModel requestBody)
        {
          
                var transaction = _unitOfWork.Transaction();
                BirdCageType existedBirdCageType = (await _unitOfWork.BirdCageTypeRepository.GetByIdAsync(requestBody.BirdCageTypeId));
                if (existedBirdCageType == null)
                {
                    throw new Exception("BirdCageType Id does not exist in the system.");
                }
                Formula formula = new Formula()
                {
                    Code = requestBody.Code,
                    MinWidth = requestBody.MinWidth,
                    MaxBars = requestBody.MaxBars,
                    MaxHeight = requestBody.MaxHeight,
                    MinHeight = requestBody.MinHeight,
                    MaxWidth = requestBody.MaxWidth,
                    MinBars = requestBody.MinBars,
                    Price = requestBody.Price,
                    BirdCageTypeId = requestBody.BirdCageTypeId,
                    ConstructionTime = requestBody.ConstructionTime,
                };
            await _unitOfWork.FormulaRepository.AddAsync(formula);
            await _unitOfWork.SaveChangesAsync();

            foreach (var item in requestBody.Specifications)
                {
                    var specifications = await _unitOfWork.SpecificationRepository.FirstOrDefaultAsync(p => p.Id == item);

                    if (specifications == null)
                    {
                        throw new Exception("BirdCageType Id does not exist in the system.");
                    }
                    FormulaSpecification formulaSpecification = new FormulaSpecification
                    {
                        FormulaId = formula.Id,
                        SpecificationId = specifications.Id
                    };

                    await _unitOfWork.FormulaSpecificationRepository.AddAsync(formulaSpecification);
                    await _unitOfWork.SaveChangesAsync();
                }

                
                await _unitOfWork.SaveChangesAsync();
                transaction.Commit();
            }
        
        public async Task<Pagination<FormulaViewModel>> GetPageAsync(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.FormulaRepository.GetPaginationAsync(pageIndex, pageSize);
            return _mapper.Map<Pagination<FormulaViewModel>>(result);
        }
        public async Task<List<FormulaViewModel>> GetAllFromulaAsync()
        {
            try
            {
                IEnumerable<Formula> formulas = await _unitOfWork.FormulaRepository.GetAllAsync();
                List<FormulaViewModel> formulaViewModels = _mapper.Map<List<FormulaViewModel>>(
                     (await _unitOfWork.FormulaRepository.GetAllAsync())
                     
                );

                return formulaViewModels;
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
                     (await _unitOfWork.FormulaRepository.GetAllAsync())
                     .Where(f => f.isDelete == false)
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
        public async Task<FormulaViewModel> GetFormulaById(int key)
        {
            Formula feature = await _unitOfWork.FormulaRepository.GetByIdAsync(key);
            if (feature == null)
            {
                throw new Exception("Please enter the correct information!!! ");
            }
            var result = _mapper.Map<FormulaViewModel>(feature);
            return result;
        }

        public async Task UpdateFormulaAsync(int key, UpdateFormulaViewModel updateFormulaViewModel)
        {
            try
            {
                var transaction = _unitOfWork.Transaction();
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
                existedFormula.isDelete= updateFormulaViewModel.isDelete;
                foreach (var item in updateFormulaViewModel.Specifications)
                {
                    var formulaSpecifications = await _unitOfWork.SpecificationRepository.FirstOrDefaultAsync(p => p.Id == item);

                    if (formulaSpecifications == null)
                    {
                        throw new Exception("Specifications Id does not exist in the system.");
                    }
                    FormulaSpecification formulaSpecification = new FormulaSpecification
                    {
                        FormulaId = existedFormula.Id,
                        SpecificationId = formulaSpecifications.Id
                    };
                    /* FormulaSpecification
                      _unitOfWork.FormulaSpecificationRepository.Update(formulaSpecification);
                      await _unitOfWork.SaveChangesAsync();*/
                }
                _unitOfWork.FormulaRepository.Update(existedFormula);
                await _unitOfWork.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task DeleteFormula(int key)
        {
            Formula existedFormula = await _unitOfWork.FormulaRepository.GetByIdAsync(key);

            if (existedFormula == null)
            {
                throw new Exception("Formula Id does not exist in the system.");
            }
            existedFormula.isDelete = true;
            _unitOfWork.FormulaRepository.Update(existedFormula);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateFormula(int key)
        {
            Formula existedFormula = await _unitOfWork.FormulaRepository.GetByIdAsync(key);

            if (existedFormula == null)
            {
                throw new Exception("Formula Id does not exist in the system.");
            }
            existedFormula.isDelete = false;
            _unitOfWork.FormulaRepository.Update(existedFormula);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
