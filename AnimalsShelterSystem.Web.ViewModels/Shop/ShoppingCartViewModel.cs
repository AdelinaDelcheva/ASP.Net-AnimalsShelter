using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.ViewModels.Shop
{
	public class ShoppingCartViewModel
	{
		[Required]
		public IList<ShoppingCartItemViewModel> Items { get; set; } = new List<ShoppingCartItemViewModel>();
		public decimal Total { get; set; }
		public int TotalItems { get; set; }
		
	}
}
