
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
        private readonly ICharacteristicService characteristicService;
        
        public AnimalController(IAnimalBreedService animalBreedService, IVolunteerService volunteerService,
                                IAnimalService animalService, ICharacteristicService characteristicService)
           
        {
            this.animalBreedService = animalBreedService;
            this.volunteerService = volunteerService;
            this.animalService = animalService;
            this.characteristicService = characteristicService;
           
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

                return this.NoVolunteerExistsById();
            }
            try
            {
                AnimalFormModel animalFormModel = new AnimalFormModel()
                {
                    Breeds = await this.animalBreedService.AllBreedsAsync()
                };
                return View(animalFormModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AnimalFormModel model)
        {
            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer)
            {

                return this.NoVolunteerExistsById();
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

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<AnimalAllViewModel> myAnimals =
                new List<AnimalAllViewModel>();

            string userId = this.User.GetId()!;
            bool isUserVolunteer = await this.volunteerService
                .VolunteerExistsByUserIdAsync(userId);

            try
            {
                if (isUserVolunteer)
                {
                    string? volunteerId =
                        await this.volunteerService.GetVolunteerIdByUserIdAsync(userId);

                    myAnimals.AddRange(await this.animalService.AllByVolunteerIdAsync(volunteerId!));
                }
                else
                {
                    myAnimals.AddRange(await this.animalService.AllByUserIdAsync(userId));
                }

                return this.View(myAnimals);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            AnimalDetailsViewModel? model=await this.animalService.GetDetailsByIdAsync(id);
            if(model==null)
            {
                return this.NoAnimalExistsById();
            }
            return View(model);
            
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool animalExists = await this.animalService
                .ExistsByIdAsync(id);
            if (!animalExists)
            {
                return this.NoAnimalExistsById();
            }

            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer && !User.IsAdmin())
            {

                return this.NoVolunteerExistsById();
            }

            string? volunteerId =
                    await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);



            bool isVolunteerCaretaker = await this.animalService
                .IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(id, volunteerId!);
            if (!isVolunteerCaretaker && !User.IsAdmin())
            {
                return this.VolunteerIsNotCareTaker();
            }

            try
            {
                AnimalFormModel formModel = await this.animalService
                    .GetAnimalForEditByIdAsync(id);
                formModel.Breeds = await this.animalBreedService.AllBreedsAsync();

                return this.View(formModel);
            }
            catch (Exception)
            {

                return  this.GeneralError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, AnimalFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Breeds = await this.animalBreedService.AllBreedsAsync();

                return this.View(model);
            }

            bool animalExists = await this.animalService
                .ExistsByIdAsync(id);

            if (!animalExists)
            {
                return this.NoAnimalExistsById();
            }

            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer && !User.IsAdmin() )
            {

                return this.NoVolunteerExistsById();
            }

            string? volunteerId =
                    await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);



            bool isVolunteerCaretaker = await this.animalService
                .IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(id, volunteerId!);
            if (!isVolunteerCaretaker && !User.IsAdmin())
            {
                return this.VolunteerIsNotCareTaker();
            }

            try
            {
                await this.animalService.EditAnimalByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the animal. Please try again later or contact administrator!");
                model.Breeds = await this.animalBreedService.AllBreedsAsync();

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Animal was edited successfully!";
            return this.RedirectToAction("Details", "Animal", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool animalExists = await this.animalService
               .ExistsByIdAsync(id);

            if (!animalExists)
            {
                return this.NoAnimalExistsById();
            }

            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer && !User.IsAdmin())
            {

                return this.NoVolunteerExistsById();
            }

            string? volunteerId =
                    await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);



            bool isVolunteerCaretaker = await this.animalService
                .IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(id, volunteerId!);
            if (!isVolunteerCaretaker && !User.IsAdmin())
            {
                return this.VolunteerIsNotCareTaker();
            }


            try
            {
                AnimalPreDeletedViewModel viewModel =
                    await this.animalService.GetAnimalForDeleteByIdAsync(id);

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, AnimalPreDeletedViewModel model)
        {
            bool animalExists = await this.animalService
               .ExistsByIdAsync(id);

            if (!animalExists)
            {

                return this.NoAnimalExistsById();
            }

            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer && !User.IsAdmin())
            {


                return this.NoVolunteerExistsById();
            }

            string? volunteerId =
                    await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);



            bool isVolunteerCaretaker = await this.animalService
                .IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(id, volunteerId!);
            if (!isVolunteerCaretaker && !User.IsAdmin())
            {


                return this.VolunteerIsNotCareTaker();
            }

            try
            {
                await this.animalService.DeleteAnimalByIdAsync(id);

                this.TempData[WarningMessage] = "The Animal was successfully deleted!";
                return this.RedirectToAction("Mine", "Animal");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Adopt(string id)
        {
            bool animalExists = await this.animalService
               .ExistsByIdAsync(id);

            if (!animalExists)
            {

                return this.NoAnimalExistsById();
            }

            bool isAnimalAdopted = await animalService.IsAdoptedAsync(id);
            if (isAnimalAdopted)
            {
                TempData[ErrorMessage] =
                    "Selected animal is already adopteb by another user! Please select another animal.";

                return RedirectToAction("All", "Animal");
            }

          
            try
            {
                await animalService.AdoptAnimalAsync(id, User.GetId()!);
            }
            catch (Exception)
            {
                return GeneralError();
            }

          //  this.memoryCache.Remove(RentsCacheKey);

            return RedirectToAction("Mine", "Animal");
        }

        //[HttpGet]
        //public async Task<IActionResult> AddCharacteristic(string id)
        //{
        //    bool animalExists = await this.animalService
        //        .ExistsByIdAsync(id);
        //    if (!animalExists)
        //    {
        //        this.TempData[ErrorMessage] = "Animal with the provided id does not exist!";
        //        return RedirectToAction("All", "Animal");
        //    }

        //    bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
        //    if (!isVolunteer)
        //    {

        //        this.TempData[ErrorMessage] = "You must be a volunteer in order to add a new animal characteristic!";
        //        return this.RedirectToAction("Become", "Volunteer");
        //    }
        //    string? volunteerId =
        //           await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);



        //    bool isVolunteerCaretaker = await this.animalService
        //        .IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(id, volunteerId!);
        //    if (!isVolunteerCaretaker)
        //    {
        //        this.TempData[ErrorMessage] = "You must be the volunteer caretaker of the animal you want to edit!";

        //        return this.RedirectToAction("Mine", "Animal");
        //    }

        //    try
        //    {
        //        AnimalAddCharacteristicViewModel formModel = await this.animalService.GetCharacteristicByIdAsync(id);

        //            formModel.AllCharacteristics = await this.characteristicService.AllCharacteristicsAsync();
        //            return this.View(formModel);



        //    }
        //    catch (Exception)
        //    {
        //        return this.GeneralError();
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddCharacteristic(string id, AnimalAddCharacteristicViewModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {


        //        return NotFound();
        //    }

        //    bool animalExists = await this.animalService
        //        .ExistsByIdAsync(id);

        //    if (!animalExists)
        //    {
        //        this.TempData[ErrorMessage] = "Animal with the provided id does not exist!";

        //        return this.RedirectToAction("All", "Animal");
        //    }

        //    bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
        //    if (!isVolunteer)
        //    {

        //        this.TempData[ErrorMessage] = "You must be a volunteer in order to edit the animal!";
        //        return this.RedirectToAction("Become", "Volunteer");
        //    }

        //    string? volunteerId =
        //            await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);



        //    bool isVolunteerCaretaker = await this.animalService
        //        .IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(id, volunteerId!);
        //    if (!isVolunteerCaretaker)
        //    {
        //        this.TempData[ErrorMessage] = "You must be the volunteer caretaker of the animal you want to edit!";

        //        return this.RedirectToAction("Mine", "Animal");
        //    }
        //    bool isCharactExist = await this.characteristicService.ExistsByIdAsync(model.CharacteristicId);
        //    if(!isCharactExist)
        //    {
        //        this.TempData[ErrorMessage] = "The category ID does not exist";

        //        return this.RedirectToAction("Details", "Animal", new { id });
        //    }

        //    bool isAlreadyAdded= await this.characteristicService.AlreadyAddedByIdAsync(model.CharacteristicId,id);
        //    if (isAlreadyAdded)
        //    {
        //        this.TempData[ErrorMessage] = "The characteristic is already added for that animal!";

        //        return this.RedirectToAction("Details", "Animal", new { id });
        //    }
        //    try
        //    {
        //        await this.animalService.AddAnimalCharactericticByIdAsync(id, model);
        //    }
        //    catch (Exception)
        //    {
        //        this.ModelState.AddModelError(string.Empty,
        //            "Unexpected error occurred while trying to update the animal. Please try again later or contact administrator!");
        //        model.AllCharacteristics = await this.characteristicService.AllCharacteristicsAsync();

        //        return this.View(model);
        //    }

        //    this.TempData[SuccessMessage] = "Animal characteristic was added successfully!";
        //    return this.RedirectToAction("Details", "Animal", new { id });
        //}

        //[HttpPost]
        //public async Task<IActionResult> RemoveCharacteristic(string id,int model)
        //{



        //    if (!this.ModelState.IsValid)
        //    {



        //        return NotFound();
        //    }

        //    bool animalExists = await this.animalService
        //        .ExistsByIdAsync(id);

        //    if (!animalExists)
        //    {
        //        this.TempData[ErrorMessage] = "Animal with the provided id does not exist!";

        //        return this.RedirectToAction("All", "Animal");
        //    }

        //    bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
        //    if (!isVolunteer)
        //    {

        //        this.TempData[ErrorMessage] = "You must be a volunteer in order to edit the animal!";
        //        return this.RedirectToAction("Become", "Volunteer");
        //    }

        //    string? volunteerId =
        //            await this.volunteerService.GetVolunteerIdByUserIdAsync(User.GetId()!);



        //    bool isVolunteerCaretaker = await this.animalService
        //        .IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(id, volunteerId!);
        //    if (!isVolunteerCaretaker)
        //    {
        //        this.TempData[ErrorMessage] = "You must be the volunteer caretaker of the animal you want to edit!";

        //        return this.RedirectToAction("Mine", "Animal");
        //    }
        //    bool isCharactExist = await this.characteristicService.ExistsByIdAsync(model);
        //    if (!isCharactExist)
        //    {
        //        this.TempData[ErrorMessage] = "The category ID does not exist";


        //        return this.RedirectToAction("Details", "Animal", new { id});
        //    }
        //    try
        //    {
        //        await this.animalService.RemoveAnimalCharactericticByIdAsync(model, id);
        //    }
        //    catch (Exception)
        //    {
        //        this.ModelState.AddModelError(string.Empty,
        //            "Unexpected error occurred while trying to update the animal. Please try again later or contact administrator!");
        //        return this.RedirectToAction("Details", "Animal", new { id });
        //    }

        //    this.TempData[SuccessMessage] = "Animal characteristic was removed successfully!";

        //    return this.RedirectToAction("Details", "Animal", new { id });
        //}

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return this.RedirectToAction("Index", "Home");
        }

        private IActionResult NoAnimalExistsById()
        {
            
                this.TempData[ErrorMessage] = "Animal with the provided id does not exist!";

                return this.RedirectToAction("All", "Animal");
           
        }

        private IActionResult NoVolunteerExistsById()
        {

            this.TempData[ErrorMessage] = "You must be a volunteer in order to add a new animal!";
            return this.RedirectToAction("Become", "Volunteer");

        }

        private IActionResult VolunteerIsNotCareTaker()
        {
            this.TempData[ErrorMessage] = "You must be the volunteer caretaker of the animal you want to edit!";

            return this.RedirectToAction("Mine", "Animal");
        }

       
    }
}
