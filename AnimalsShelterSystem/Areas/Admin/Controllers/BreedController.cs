using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Web.ViewModels.AnimalBreed;
using static AnimalsShelterSystem.Common.NotificationMessagesConstants;
using AnimalsShelterSystem.Web.ViewModels.Characteristic;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterSystem.Web.Areas.Admin.Controllers
{
    public class BreedController : BaseAdminController
    {
        private readonly IAnimalBreedService breedService;
        public BreedController(IAnimalBreedService breedService)
        {
            this.breedService = breedService;
        }
        public async Task<IActionResult> All()
        {
            var model = await this.breedService.AllBreedsAsync();

            return View(model);
        }


        [HttpGet]
        public IActionResult Add()
        {
            AnimalBreedInputModel model = new AnimalBreedInputModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AnimalBreedInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            try
            {
                await breedService.AddBreedInDbAsync(model);
                this.TempData[SuccessMessage] = "Breed was added successfully!";
                return this.RedirectToAction("All", "Breed", new { area = "Admin" });
            }
            catch (Exception e)
            {

                return NotFound();
            }
        }

       
    }
}
