using AnimalsShelterSystem.Services.Data;
using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Web.Infrastructure.Extensitions;
using AnimalsShelterSystem.Web.ViewModels.Animal;
using AnimalsShelterSystem.Web.ViewModels.Characteristic;
using static AnimalsShelterSystem.Common.NotificationMessagesConstants;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterSystem.Web.Controllers
{
    [Authorize]
    public class CharacteristicController : Controller
    {
        private readonly ICharacteristicService characteristicService;
        private readonly IAnimalService animalService;
        private readonly IVolunteerService volunteerService;
        public CharacteristicController(ICharacteristicService characteristicService, IAnimalService animalService, IVolunteerService volunteerService)
        {
            this.characteristicService = characteristicService;
            this.animalService = animalService;
            this.volunteerService = volunteerService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<CharacteristicViewModel> allCharacteristics=await this.characteristicService.GetAllCharacteristicsAsync();
            return View(allCharacteristics);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string information)
        {
            bool characteristicExists = await characteristicService.ExistsByIdAsync(id);
            if (!characteristicExists)
            {
                return NotFound();
            }

            CharacteristicDetailsModel viewModel =
                await characteristicService.GetDetailsByIdAsync(id);
            if (viewModel.GetUrlInformation() != information)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddCharacteristic(string id)
        {
            bool animalExists = await this.animalService
                .ExistsByIdAsync(id);
            if (!animalExists)
            {
                this.TempData[ErrorMessage] = "Animal with the provided id does not exist!";
                return RedirectToAction("All", "Animal");
            }

            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer && !User.IsAdmin())
            {

                this.TempData[ErrorMessage] = "You must be a volunteer in order to add a new animal characteristic!";
                return this.RedirectToAction("Become", "Volunteer");
            }
            string? volunteerId =
                   await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);



            bool isVolunteerCaretaker = await this.animalService
                .IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(id, volunteerId!);
            if (!isVolunteerCaretaker && !User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the volunteer caretaker of the animal you want to edit!";

                return this.RedirectToAction("Mine", "Animal");
            }

            try
            {
                AnimalAddCharacteristicViewModel formModel = await this.animalService.GetCharacteristicByIdAsync(id);

                formModel.AllCharacteristics = await this.characteristicService.AllCharacteristicsAsync();
                return this.View(formModel);



            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacteristic(string id, AnimalAddCharacteristicViewModel model)
        {
            if (!this.ModelState.IsValid)
            {


                return NotFound();
            }

            bool animalExists = await this.animalService
                .ExistsByIdAsync(id);

            if (!animalExists)
            {
                this.TempData[ErrorMessage] = "Animal with the provided id does not exist!";

                return this.RedirectToAction("All", "Animal");
            }

            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer && !User.IsAdmin())
            {

                this.TempData[ErrorMessage] = "You must be a volunteer in order to edit the animal!";
                return this.RedirectToAction("Become", "Volunteer");
            }

            string? volunteerId =
                    await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);



            bool isVolunteerCaretaker = await this.animalService
                .IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(id, volunteerId!);
            if (!isVolunteerCaretaker && !User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the volunteer caretaker of the animal you want to edit!";

                return this.RedirectToAction("Mine", "Animal");
            }
            bool isCharactExist = await this.characteristicService.ExistsByIdAsync(model.CharacteristicId);
            if (!isCharactExist)
            {
                this.TempData[ErrorMessage] = "The category ID does not exist";

                return this.RedirectToAction("Details", "Animal", new { id });
            }

            bool isAlreadyAdded = await this.characteristicService.AlreadyAddedByIdAsync(model.CharacteristicId, id);
            if (isAlreadyAdded)
            {
                this.TempData[ErrorMessage] = "The characteristic is already added for that animal!";

                return this.RedirectToAction("Details", "Animal", new { id });
            }
            try
            {
                await this.animalService.AddAnimalCharactericticByIdAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the animal. Please try again later or contact administrator!");
                model.AllCharacteristics = await this.characteristicService.AllCharacteristicsAsync();

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Animal characteristic was added successfully!";
            return this.RedirectToAction("Details", "Animal", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCharacteristic(string id, int model)
        {



            if (!this.ModelState.IsValid)
            {



                return NotFound();
            }

            bool animalExists = await this.animalService
                .ExistsByIdAsync(id);

            if (!animalExists)
            {
                this.TempData[ErrorMessage] = "Animal with the provided id does not exist!";

                return this.RedirectToAction("All", "Animal");
            }

            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer && !User.IsAdmin())
            {

                this.TempData[ErrorMessage] = "You must be a volunteer in order to edit the animal!";
                return this.RedirectToAction("Become", "Volunteer");
            }

            string? volunteerId =
                    await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);



            bool isVolunteerCaretaker = await this.animalService
                .IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(id, volunteerId!);
            if (!isVolunteerCaretaker && !User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the volunteer caretaker of the animal you want to edit!";

                return this.RedirectToAction("Mine", "Animal");
            }
            bool isCharactExist = await this.characteristicService.ExistsByIdAsync(model);
            if (!isCharactExist)
            {
                this.TempData[ErrorMessage] = "The category ID does not exist";


                return this.RedirectToAction("Details", "Animal", new { id });
            }
            try
            {
                await this.animalService.RemoveAnimalCharactericticByIdAsync(model, id);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the animal. Please try again later or contact administrator!");
                return this.RedirectToAction("Details", "Animal", new { id });
            }

            this.TempData[SuccessMessage] = "Animal characteristic was removed successfully!";

            return this.RedirectToAction("Details", "Animal", new { id });
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
