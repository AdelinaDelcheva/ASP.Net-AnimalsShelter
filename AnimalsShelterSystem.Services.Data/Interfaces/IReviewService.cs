
namespace AnimalsShelterSystem.Services.Data.Interfaces
{
    using AnimalsShelterSystem.Web.ViewModels.Review;
    public interface IReviewService
    {
        Task<AddReviewViewModel?> GetReviewAddFormModelAsync(string animalId);

        Task AddAsync(string id,AddReviewViewModel viewModel,string user);
    }
}
