
using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
    public interface IVourcherService : IBaseService
    {
        Task<IEnumerable<VourcherViewModel>> GetVourcherAsync();
    }
}
