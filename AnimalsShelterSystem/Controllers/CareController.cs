using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Web.ViewModels.Animal;
using static AnimalsShelterSystem.Common.NotificationMessagesConstants;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterSystem.Web.Controllers
{
    [Authorize]
    public class CareController : Controller
    {
        private readonly IAnimalService animalService;
        private readonly ICareService careService;
        public CareController(IAnimalService animalService,ICareService careService)
        {
            this.animalService = animalService;
            this.careService = careService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<AnimalForShoppingCartViewModel> cares= await careService.GetAllCaresAsync();

            return View(cares);
        }

        public async Task<IActionResult> Details(int id)
        {
            bool existById=await careService.ExistByIdAsync(id);
            if(!existById)
            {
                this.TempData[ErrorMessage] = "Care with the provided id does not exist!";

                return this.RedirectToAction("All", "Care");
            }

            var model = await careService.GetCareDetailsAsync(id);

            return View(model);
        }
    }
}
