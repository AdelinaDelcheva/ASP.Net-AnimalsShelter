
namespace AnimalsShelterSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Common.NotificationMessagesConstants;
    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Web.Infrastructure.Extensitions;
    using AnimalsShelterSystem.Web.ViewModels.Animal;


    [Authorize]
    public class AnimalController : Controller
    {
        private readonly IAnimalBreedService animalBreedService;
        private readonly IVolunteerService volunteerService;
        public AnimalController(IAnimalBreedService animalBreedService, IVolunteerService volunteerService)
        {
            this.animalBreedService = animalBreedService;
            this.volunteerService = volunteerService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
          
            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer)
            {

                this.TempData[ErrorMessage] = "You must be a volunteer in order to add a new animal!";
                return this.RedirectToAction("Become", "Volunteer");
            }

            AnimalFormModel animalFormModel = new AnimalFormModel()
            {
                Breeds = await this.animalBreedService.AllBreedsAsync()
            };
            return View(animalFormModel);
        }
    }
}
