using AnimalsShelterSystem.Services.Data;
using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Web.Infrastructure.Extensitions;
using AnimalsShelterSystem.Web.ViewModels.Characteristic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterSystem.Web.Controllers
{
    [Authorize]
    public class CharacteristicController : Controller
    {
        private readonly ICharacteristicService characteristicService;
        public CharacteristicController(ICharacteristicService characteristicService)
        {
            this.characteristicService = characteristicService;
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
    }
}
