
namespace AnimalsShelterSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Common.NotificationMessagesConstants;
    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Web.Infrastructure.Extensitions;
    using AnimalsShelterSystem.Web.ViewModels.Animal;
    using AnimalsShelterSystem.Services.Data.Models.Animal;

    [Authorize]
    public class AnimalController : Controller
    {
        private readonly IAnimalBreedService animalBreedService;
        private readonly IVolunteerService volunteerService;
        private readonly IAnimalService animalService;
        public AnimalController(IAnimalBreedService animalBreedService, IVolunteerService volunteerService,
                                IAnimalService animalService)
        {
            this.animalBreedService = animalBreedService;
            this.volunteerService = volunteerService;
            this.animalService = animalService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllAnimalsQueryModel model)
        {
            AllAnimalFilteredAndPagedServiceModel serviceModel=
                 await this.animalService.AllAsync(model);
            model.Animals = serviceModel.Animals;
            model.TotalAnimals = serviceModel.TotalAnimalsCount;
            model.Breeds = await this.animalBreedService.AllBreedNamesAsync();
            return View(model);
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

        [HttpPost]
        public async Task<IActionResult> Add(AnimalFormModel model)
        {
            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer)
            {

                this.TempData[ErrorMessage] = "You must be a volunteer in order to add a new animal!";
                return this.RedirectToAction("Become", "Volunteer");
            }


            bool breedExists =
                await this.animalBreedService.ExistsByIdAsync(model.BreedId);
            if (!breedExists)
            {
                // Adding model error to ModelState automatically makes ModelState Invalid
                this.ModelState.AddModelError(nameof(model.BreedId), "Selected breed does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.Breeds = await this.animalBreedService.AllBreedsAsync();

                return this.View(model);
            }

            try
            {
                string? volunteerId =
                    await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);

                string animalId =
                    await this.animalService.CreateAndReturnIdAsync(model, volunteerId!);

                this.TempData[SuccessMessage] = "Animal was added successfully!";
                return this.RedirectToAction("Details", "Animal", new { id = animalId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new animal! Please try again later or contact administrator!");
                model.Breeds = await this.animalBreedService.AllBreedsAsync();

                return this.View(model);
            }
        }

    }
}
