using AutoMapper;
using BirdCageShopInterface;
using BirdCageShopInterface.IServices;
using BirdCageShopInterface.IValidator;
using BirdCageShopViewModel.Category;
using BirdCageShopViewModel.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class CategoryService : BaseService, ICategoryService
    {

        //private readonly ICate _roleValidator;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }
    }
}
