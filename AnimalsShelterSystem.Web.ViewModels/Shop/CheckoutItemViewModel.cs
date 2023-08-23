using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.ViewModels.Shop
{
	public class CheckoutItemViewModel
	{
		[Required]
		public string Id { get; set; } = null!;
		
		public int CareId { get; set; } 

		[Required]
		public int Quantity { get; set; }

		public decimal Price { get; set; }

		
	}
}
