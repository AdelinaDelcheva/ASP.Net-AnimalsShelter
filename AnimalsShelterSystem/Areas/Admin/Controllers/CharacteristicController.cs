using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Web.ViewModels.Characteristic;
using Microsoft.AspNetCore.Mvc;

using static AnimalsShelterSystem.Common.NotificationMessagesConstants;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AnimalsShelterSystem.Web.Areas.Admin.Controllers
{
    public class CharacteristicController : BaseAdminController
    {
        private readonly ICharacteristicService characteristicService;
        public CharacteristicController(ICharacteristicService characteristicService)
        {
            this.characteristicService = characteristicService;
        }
        [HttpGet]
        public IActionResult Add()
        {
            CharacteristicFormModel model=new CharacteristicFormModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CharacteristicFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           

            try
            {
                await characteristicService.AddCharacteristInDbAsync(model);
                this.TempData[SuccessMessage] = "Characteristic was added successfully!";
                return this.RedirectToAction("All", "Characteristic", new { area = "" });
            }
            catch (Exception e)
            {
               
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("All", "Characteristic", new { area = "" });
            }
            if(!await this.characteristicService.ExistsByIdAsync(id))
            {
                this.TempData[ErrorMessage] = "Characteristic with the provided id does not exist!";

                return this.RedirectToAction("All", "Characteristic", new { area = "" });
            }
            try
            {
                await characteristicService.DeleteCharacteristInDbAsync(id);
                this.TempData[SuccessMessage] = "Characteristic with the provided id was deleted successfully!";
                return this.RedirectToAction("All", "Characteristic", new { area = "" });
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}
