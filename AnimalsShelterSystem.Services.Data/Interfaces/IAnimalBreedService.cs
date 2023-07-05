
namespace AnimalsShelterSystem.Services.Data.Interfaces
{
    using AnimalsShelterSystem.Web.ViewModels.AnimalBreed;
    public interface IAnimalBreedService
    {
        Task<IEnumerable<AnimalBreedFormModel>> AllCategoriesAsync();
    }
}
