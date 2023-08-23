using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Services.Data.Models.ShoppingCart
{
	public class ProductForCartDto
	{
		public string Id { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string Image { get; set; } = null!;
		public string Breed { get; set; } = null!;
		public int Age { get; set; }
		public int CareId { get; set; }
		public string CareName { get; set; } = null!;
        public decimal Price { get; set; }
		
	}
}
