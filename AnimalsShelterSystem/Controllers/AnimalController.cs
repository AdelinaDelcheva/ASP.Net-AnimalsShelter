
namespace AnimalsShelterSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class AnimalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
