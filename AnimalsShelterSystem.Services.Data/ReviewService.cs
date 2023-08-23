

namespace AnimalsShelterSystem.Services.Data
{
    using AnimalsShelterSystem.Data.Models;
    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Web.Data;
    using AnimalsShelterSystem.Web.ViewModels.Review;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class ReviewService : IReviewService
    {
        private readonly AnimalsShelterDbContext dbContext;
        public ReviewService(AnimalsShelterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(string id, AddReviewViewModel viewModel,string user)
        {
            Review review = new Review()
            {
                AnimalId = Guid.Parse(id),
                Text = viewModel.Text,
                CreatorId = Guid.Parse(user),
                LastModified = DateTime.Now,

            };
            await dbContext.Reviews.AddAsync(review);
            await dbContext.SaveChangesAsync();
        }

        public async Task<AddReviewViewModel?> GetReviewAddFormModelAsync(string animalId)
        {
            return await dbContext.Animals
                .Select(a=>new AddReviewViewModel()
                {
                    AnimalId = a.Id.ToString(),
                    AnimalName= a.Name
                }).FirstOrDefaultAsync(a=>a.AnimalId== animalId);
        }
    }
}
