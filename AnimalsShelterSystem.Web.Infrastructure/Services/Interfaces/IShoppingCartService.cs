using AnimalsShelterSystem.Services.Data.Models.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.Infrastructure.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> AddOneItemToShoppingCart(string animalId,int careId);

        ShoppingCart RemoveOneItemFromShoppingCart(string productId, int careId);

        Task<ShoppingCart> GetShoppingCart();

        int TotalItems();

        ShoppingCart DeleteItemFromShoppingCart(string productId, int careId);

        void EmptyShoppingCart();
    }
}
