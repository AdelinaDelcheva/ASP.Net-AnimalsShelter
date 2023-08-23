using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Services.Data.Models.ShoppingCart;
using AnimalsShelterSystem.Web.Infrastructure.Extensitions;
using AnimalsShelterSystem.Web.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.Infrastructure.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAnimalService animalService;
        private readonly ICareService careService;
        public ShoppingCartService(IHttpContextAccessor httpContextAccessor, IAnimalService productService, ICareService careService)
        {
            _httpContextAccessor = httpContextAccessor;
            animalService = productService;
            this.careService = careService;
        }

        public async Task<ShoppingCart> AddOneItemToShoppingCart(string animalId,int careId)
        {

			
		

            ShoppingCart? cart = _httpContextAccessor.HttpContext.Session.Get<ShoppingCart>("cart");

            if (cart == null)
            {
                cart = new ShoppingCart();
            }

            if (!await animalService.ExistsByIdAsync(animalId))
            {
                throw new InvalidOperationException();
            }

			if (!await careService.ExistByIdAsync(careId))
			{
				throw new InvalidOperationException();
			}

			var product = cart.Items.FirstOrDefault(i => i.Id == animalId && i.CareId==careId);
            if (product == null)
            {
                product = new ShoppingCartItem()
                {
                    Id = animalId,
                    CareId = careId,
                    Quantity = 0
                };
                cart.Items.Add(product);

            }

            product.Quantity++;


            _httpContextAccessor.HttpContext.Session.Set("cart", cart);

            return cart;

        }

        public async Task<ShoppingCart> GetShoppingCart()
        {
            ShoppingCart? cart = _httpContextAccessor.HttpContext.Session.Get<ShoppingCart>("cart");


            var newCart = new ShoppingCart();

            if (cart != null)
            {
                foreach (var item in cart.Items)
                {
                    if (await animalService.ExistsByIdAsync(item.Id) && await careService.ExistByIdAsync(item.CareId))
                    {
                        newCart.Items.Add(item);

                    }
                }
            }


            return newCart;
        }

        public ShoppingCart RemoveOneItemFromShoppingCart(string productId, int careId)
        {
            ShoppingCart? cart = _httpContextAccessor.HttpContext.Session.Get<ShoppingCart>("cart");
            if (cart == null)
            {
                throw new InvalidOperationException();
            }

            var product = cart.Items.FirstOrDefault(i => i.Id == productId && i.CareId==careId);
            if (product == null)
            {
                throw new InvalidOperationException();

            }
            if (product.Quantity > 1)
            {
                product.Quantity--;


            }


            _httpContextAccessor.HttpContext.Session.Set("cart", cart);

            return cart;
        }

        public ShoppingCart DeleteItemFromShoppingCart(string productId, int careId)
        {
            ShoppingCart? cart = _httpContextAccessor.HttpContext.Session.Get<ShoppingCart>("cart");
            if (cart == null)
            {
                throw new InvalidOperationException();
            }

            var product = cart.Items.FirstOrDefault(i => i.Id == productId && i.CareId == careId);
            if (product == null)
            {
                throw new InvalidOperationException();

            }

            cart.Items.Remove(product);

            _httpContextAccessor.HttpContext.Session.Set("cart", cart);

            return cart;
        }

        public void EmptyShoppingCart()
        {
            var cart = new ShoppingCart();
            _httpContextAccessor.HttpContext.Session.Set("cart", cart);
        }

        public int TotalItems()
        {
            ShoppingCart? cart = _httpContextAccessor.HttpContext.Session.Get<ShoppingCart>("cart");
            if (cart == null)
            {
                return 0;
            }
            return cart.Items.Select(i => i.Quantity).Sum();
        }
    }
}
