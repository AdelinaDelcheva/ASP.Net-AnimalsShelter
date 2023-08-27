using AnimalsShelterSystem.Services.Data.Models.ShoppingCart;
using AnimalsShelterSystem.Web.ViewModels.Order;
using AnimalsShelterSystem.Web.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Services.Data.Interfaces
{
	public interface IOrderService
	{
		Task<ShoppingCartViewModel> GetShoppingCartForCheckoutAsync(ShoppingCart cart);

		Task<bool> CartValidAsync(ShoppingCartViewModel model);

		CheckoutFormViewModel GetCheckoutInfo(ShoppingCartViewModel cart);
		Task PlaceOrderAsync(CheckoutFormViewModel model, string userId);
		Task<IEnumerable<OrderListViewModel>> AllByIdAsync(string userId);

		Task<IEnumerable<OrderInputViewModel>> AllAsync();

	}
}
