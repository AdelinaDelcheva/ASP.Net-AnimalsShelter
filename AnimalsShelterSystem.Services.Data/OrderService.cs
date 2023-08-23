using AnimalsShelterSystem.Data.Models;
using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Services.Data.Models.ShoppingCart;
using AnimalsShelterSystem.Web.Data;
using AnimalsShelterSystem.Web.ViewModels.Order;
using AnimalsShelterSystem.Web.ViewModels.Shop;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Services.Data
{
	public class OrderService:IOrderService
	{
		private readonly IAnimalService animalService;
		private readonly ICareService careService;
		private readonly AnimalsShelterDbContext dbContext;
		public OrderService(IAnimalService animalService, AnimalsShelterDbContext dbContext, ICareService careService)
		{
			this.animalService = animalService;
			this.dbContext = dbContext;
			this.careService = careService;
		}

		public async Task<ShoppingCartViewModel> GetShoppingCartForCheckoutAsync(ShoppingCart cart)
		{
			ShoppingCartViewModel model = new ShoppingCartViewModel();
			IList<ShoppingCartItem> items = cart.Items;


            var products = await animalService.GetProductsForCart(items);

			foreach (var item in products)
			{
				ShoppingCartItemViewModel itemModel = new ShoppingCartItemViewModel()
				{
					Id = item.Id,
					Name = item.Name,
					Price = item.Price,
					CareId=item.CareId,
					CareName=item.CareName,
					Image = item.Image,
					Breed= item.Breed,
					Age= item.Age,
					Quantity = cart.Items.First(i => i.Id == item.Id && i.CareId==item.CareId).Quantity
				};

				model.Items.Add(itemModel);

				model.Total = model.Items.Select(i => i.Quantity * i.Price).Sum();
				model.TotalItems = model.Items.Select(i => i.Quantity).Sum();
				

			}

			return model;
		}

		public async Task<bool> CartValidAsync(ShoppingCartViewModel model)
		{
			foreach (var item in model.Items)
			{
				if (!await animalService.ExistsByIdAsync(item.Id) || !await careService.ExistByIdAsync(item.CareId))
				{
					return false;
				}
			}
			return true;


		}

		public CheckoutFormViewModel GetCheckoutInfo(ShoppingCartViewModel cart)
		{
			var model = new CheckoutFormViewModel();
			model.Items = cart.Items
				.Select(i => new CheckoutItemViewModel()
				{
					Id = i.Id,
					CareId=i.CareId,
					Price=i.Price,
					Quantity = i.Quantity,
					
				}).ToArray();
			
			return model;
		}
		public async Task PlaceOrderAsync(CheckoutFormViewModel model, string userId)
		{


			var order = new Order
			{
				CreationDate=DateTime.Now,
				FirstName=model.FirstName,
				LastName=model.LastName,
				CreatorId = Guid.Parse(userId),
				PhoneNumber=model.Phone,
				City=model.City,
				Country=model.Country,
				ZipCode=model.ZipCode,
				StreetAddress=model.StreetAddress,
				
				
				Orders = model.Items.Select(i => new OrderedCare()
				{
					AnimalId = Guid.Parse(i.Id),
					CareId=i.CareId,
					Price=i.Price,
					Quantity = i.Quantity
				}).ToArray(),
				
				
			};

			await this.dbContext.Orders.AddAsync(order);
			await this.dbContext.SaveChangesAsync();


		}

		public async Task<IEnumerable<OrderListViewModel>> AllByIdAsync(string userId)
		{
			var orders = await dbContext
				.Orders
				.Where(o => o.CreatorId.ToString() == userId)
				.Select(o => new OrderListViewModel()
				{
					FirstName = o.FirstName,
					LastName = o.LastName,
					StreetAddress = o.StreetAddress,
					City = o.City,
					Country = o.Country,
					ZipCode = o.ZipCode,
					
					Date = o.CreationDate,
					PhoneNumber = o.PhoneNumber,
					Total = o.Orders.Select(op => op.Care.Price * op.Quantity).Sum(),
					Products = o.Orders
								.Select(op => new OrderProductListViewModel()
								{
									ProductId = op.AnimalId.ToString(),
									Price = op.Care.Price,
									ImageId = op.Animal.ImageUrl,
									AnimalName = op.Animal.Name,
									CareName= op.Care.CareTypes.ToString(),
									Quantity = op.Quantity,
									CareId=op.CareId

								})
				})
				.OrderByDescending(o => o.Date)
				.ToArrayAsync();

			return orders;
		}
	}
}
