using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Services.Data.Models.ShoppingCart
{
    public class ShoppingCart
    {
        public IList<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
