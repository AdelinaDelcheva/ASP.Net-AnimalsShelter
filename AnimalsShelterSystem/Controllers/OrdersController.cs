using AnimalsShelterSystem.Services.Data;
using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Web.Infrastructure.Services;
using AnimalsShelterSystem.Web.Infrastructure.Services.Interfaces;
using AnimalsShelterSystem.Web.ViewModels.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static AnimalsShelterSystem.Common.NotificationMessagesConstants;

namespace AnimalsShelterSystem.Web.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		public readonly IShoppingCartService shopCartService;
		public readonly IOrderService orderService;
		public OrdersController(IShoppingCartService shopCartService, IOrderService orderService)
		{
			this.shopCartService = shopCartService;
			this.orderService = orderService;
		}

		[HttpGet]
	
		public async Task<IActionResult> ShoppingCart()
		{

			var cart = await shopCartService.GetShoppingCart();
			var model = await orderService.GetShoppingCartForCheckoutAsync(cart);

			return View(model);





		}
		
		[HttpGet]
		public async Task<IActionResult> AddToCart(string productId,int careId)
		{
			try
			{
				
				var newCart = await shopCartService.AddOneItemToShoppingCart(productId, careId);
				var model = await orderService.GetShoppingCartForCheckoutAsync(newCart);

				return Json(model);

				

				

			}
			catch (Exception e)
			{
				//_logger.LogError(e, "Exception thrown at {Time}", DateTime.UtcNow);
				return NotFound();
			}
		}

		[HttpGet]

		public async Task<IActionResult> RemoveFromCart(string productId,int careId)
		{
			try
			{

				var newCart = shopCartService.RemoveOneItemFromShoppingCart(productId,careId);
				var model = await orderService.GetShoppingCartForCheckoutAsync(newCart);
				return Json(model);
			}
			catch (Exception e)
			{

				return NotFound();
			}
		}

		[HttpGet]

		public async Task<IActionResult> DeleteFromCart(string productId,int careId)
		{
			try
			{

				var newCart = shopCartService.DeleteItemFromShoppingCart(productId,careId);
				var model = await orderService.GetShoppingCartForCheckoutAsync(newCart);

				return PartialView("ShoppingCart", model);
			}
			catch (Exception e)
			{

				return NotFound();
			}
		}

		[HttpPost]

		public async Task<IActionResult> ShoppingCart(ShoppingCartViewModel model)
		{
			if (!ModelState.IsValid)
			{
				
				return View(model);
			}
			try
			{
				if (!await orderService.CartValidAsync(model))
				{
					ModelState.AddModelError("cart", "Cart contents are not valid.");
					return View(model);
				}

				var checkOutModel = orderService.GetCheckoutInfo(model);

				return View("Checkout", checkOutModel);
			}
			catch (Exception e)
			{

				return NotFound();
			}


		}

		[HttpPost]
		
		public async Task<IActionResult> Checkout(CheckoutFormViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}


			//if (User?.Identity?.IsAuthenticated ?? false)
			//{
			//	model.Email = User.FindFirstValue(ClaimTypes.Email);
			//}

			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			try
			{
				await orderService.PlaceOrderAsync(model, userId);


				shopCartService.EmptyShoppingCart();



				return RedirectToAction("All", "Animal");
			}
			catch (Exception e)
			{

				return NotFound();
			}

		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			try
			{
				string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var model = await orderService.AllByIdAsync(userId);
				return View(model);
			}
			catch (Exception e)
			{
				
				return NotFound();
			}

		}
	}
}
