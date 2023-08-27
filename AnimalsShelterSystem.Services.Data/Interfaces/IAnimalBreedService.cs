
namespace AnimalsShelterSystem.Services.Data.Interfaces
{
    using AnimalsShelterSystem.Web.ViewModels.AnimalBreed;
    public interface IAnimalBreedService
    {
        Task<IEnumerable<AnimalBreedFormModel>> AllBreedsAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllBreedNamesAsync();

        Task AddBreedInDbAsync(AnimalBreedInputModel model);

        
    }
}
