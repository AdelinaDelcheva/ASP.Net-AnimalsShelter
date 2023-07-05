
namespace AnimalsShelterSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class AnimalController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
