

using AnimalsShelterSystem.Web.ViewModels.Home;

namespace AnimalsShelterSystem.Services.Data.Interfaces
{
   
    public interface IAnimalService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeAnimals();
    }
}
