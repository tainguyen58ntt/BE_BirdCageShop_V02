using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopInterface.IServices
{
	public interface IShoppingCartService: IBaseService
	{

		Task<IEnumerable<ShoppingCartViewModel>> GetShoppingCartsAsync();
	}
}
