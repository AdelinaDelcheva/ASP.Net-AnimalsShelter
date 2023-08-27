using AnimalsShelterSystem.Services.Data;
using AnimalsShelterSystem.Services.Data.Interfaces;
using static AnimalsShelterSystem.Common.NotificationMessagesConstants;
using AnimalsShelterSystem.Web.ViewModels.Characteristic;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterSystem.Web.Areas.Admin.Controllers
{
    public class OrdersController : BaseAdminController
    {
        private readonly IOrderService orderService;
        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        
        public async Task<IActionResult> All()
        {
           var model=await this.orderService.AllAsync();
            return View(model);
        }
       
	}
}
