



namespace AnimalsShelterSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;


    using AnimalsShelterSystem.Web.ViewModels.Home;
    using AnimalsShelterSystem.Services.Data.Interfaces;
    using static Common.GeneralApplicationConstants;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalService animalService;
        public HomeController(ILogger<HomeController> logger, IAnimalService animalService)
        {
            _logger = logger;
            this.animalService = animalService;
        }

        public async Task<IActionResult> Index()
        {

            if (this.User.IsInRole(AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }
            IEnumerable<IndexViewModel> lastthree = await this.animalService.LastThreeAnimals();

            return View(lastthree);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if(statusCode==400 || statusCode == 404)
            {
                return View("Error404");
            }
            return View();
        }
    }
}