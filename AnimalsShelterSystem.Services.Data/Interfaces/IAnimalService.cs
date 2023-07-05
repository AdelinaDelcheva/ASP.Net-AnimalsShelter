

using AnimalsShelterSystem.Web.ViewModels.Animal;
using AnimalsShelterSystem.Web.ViewModels.Home;

namespace AnimalsShelterSystem.Services.Data.Interfaces
{
   
    public interface IAnimalService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeAnimals();

        Task<string> CreateAndReturnIdAsync(AnimalFormModel model, string volunteerId);
    }
}
