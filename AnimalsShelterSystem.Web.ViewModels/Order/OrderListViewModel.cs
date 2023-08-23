using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.ViewModels.Order
{
	public class OrderListViewModel
	{
		public IEnumerable<OrderProductListViewModel> Products { get; set; } = new HashSet<OrderProductListViewModel>();


		public DateTime Date { get; set; }
		public decimal Total { get; set; }

	
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string Country { get; set; } = null!;
		public string City { get; set; } = null!;
		public string StreetAddress { get; set; } = null!;
		public string? ZipCode { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

	}
}
