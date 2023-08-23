using AnimalsShelterSystem.Services.Data.Interfaces;
using static AnimalsShelterSystem.Common.NotificationMessagesConstants;
using AnimalsShelterSystem.Web.Infrastructure.Extensitions;
using AnimalsShelterSystem.Web.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterSystem.Web.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IAnimalService animalService;
        private readonly IReviewService reviewService;

        public ReviewController(IAnimalService animalService, IReviewService reviewService)
        {
            this.animalService = animalService;
            this.reviewService = reviewService;
        }

       
        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            var model = await reviewService.GetReviewAddFormModelAsync(id);
            if(model != null) 
            {
                return View(model);
            }

            return this.RedirectToAction("Details", "Animal", new { id });

        }
        
        [HttpPost]
    
        public async Task<IActionResult> Add(string id,AddReviewViewModel model)
        {
            await reviewService.AddAsync(id,model, this.User.GetId());
            this.TempData[SuccessMessage] = "The Review was successfully added!";

            return this.RedirectToAction("Details", "Animal", new { id });

        }
    }
}
