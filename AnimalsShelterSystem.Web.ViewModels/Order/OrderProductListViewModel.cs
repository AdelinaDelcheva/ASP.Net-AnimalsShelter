using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.ViewModels.Order
{
	public class OrderProductListViewModel
	{
		public string ImageId { get; set; } = null!;
		public string AnimalName { get; set; } = null!;
		public int Quantity { get; set; }

		public string ProductId { get; set; } = null!;

		public decimal Price { get; set; }

		public int Id { get; set; }
		public int CareId { get; set; }
		public string CareName { get; set; } = null!;

		public bool Removed { get; set; }
	}
}
