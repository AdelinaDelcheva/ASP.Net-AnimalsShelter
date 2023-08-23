using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Services.Data.Models.ShoppingCart
{
    public class ShoppingCartItem
    {
        public string Id { get; set; } = null!;
       public int CareId { get; set; }
        public int Quantity { get; set; }
    }
}
