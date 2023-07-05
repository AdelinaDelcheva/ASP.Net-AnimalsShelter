
namespace AnimalsShelterSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using AnimalsShelterSystem.Web.Infrastructure.Extensitions;
   

    using static Common.NotificationMessagesConstants;
    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Web.ViewModels.Volunteeer;

    [Authorize]
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService volunteerService;
        public VolunteerController(IVolunteerService volunteerService)
        {
            this.volunteerService = volunteerService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(userId);
            if (isVolunteer)
            {
                this.TempData[ErrorMessage] = "You are already a volunteer!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeVolunteerFormModel model)
        {
            string? userId = this.User.GetId();
            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(userId);
            if (isVolunteer)
            {
                this.TempData[ErrorMessage] = "You are already an agent!";

                return this.RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken =
                await this.volunteerService.VolunteerExistsByUserIdAsync(model.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                this.ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with the provided phone number already exists!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

           

            try
            {
                await this.volunteerService.Create(userId, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] =
                    "Unexpected error occurred while registering you as an agent! Please try again later or contact administrator.";

                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("All", "Animal");
        }
    }
}
