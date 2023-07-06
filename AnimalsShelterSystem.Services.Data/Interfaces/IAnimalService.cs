

using AnimalsShelterSystem.Services.Data.Models.Animal;
using AnimalsShelterSystem.Web.ViewModels.Animal;
using AnimalsShelterSystem.Web.ViewModels.Home;

namespace AnimalsShelterSystem.Services.Data.Interfaces
{
   
    public interface IAnimalService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeAnimals();

        Task<string> CreateAndReturnIdAsync(AnimalFormModel model, string volunteerId);

        Task<AllAnimalFilteredAndPagedServiceModel> AllAsync(AllAnimalsQueryModel model);
        Task<IEnumerable<AnimalAllViewModel>> AllByVolunteerIdAsync(string volunteerId);

        Task<IEnumerable<AnimalAllViewModel>> AllByUserIdAsync(string userId);
        //Task<bool> ExistsByIdAsync(string animalId);

        Task<AnimalDetailsViewModel?> GetDetailsByIdAsync(string animalId);
    }
}
