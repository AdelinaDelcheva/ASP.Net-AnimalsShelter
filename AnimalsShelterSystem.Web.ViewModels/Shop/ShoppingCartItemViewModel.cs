using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.ViewModels.Shop
{
	public class ShoppingCartItemViewModel
	{
		[Required]
		public string Id { get; set; } = null!;

		public int CareId { get; set; }
        public string CareName { get; set; } = null!;
        public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public string Breed { get; set; } = null!;
		public int Age { get; set; }

		public string Image { get; set; } = null!;
		[Required]
		public int Quantity { get; set; }
	}
}
