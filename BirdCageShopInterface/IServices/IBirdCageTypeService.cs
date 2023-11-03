using birdcageshopinterface.IServices;
using BirdCageShopViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IBirdCageTypeService : IBaseService
    {

        Task<IEnumerable<ProductViewModel>> GetBirdCageTypeAsync();
    }
}
