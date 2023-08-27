using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.ViewModels.Order
{
	public class OrderInputViewModel:OrderListViewModel
	{
		public string Id { get; set; } = null!;
	}
}
