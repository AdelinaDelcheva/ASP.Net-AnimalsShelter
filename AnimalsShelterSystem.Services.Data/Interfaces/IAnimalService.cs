

using AnimalsShelterSystem.Services.Data.Models.Animal;
using AnimalsShelterSystem.Services.Data.Models.Statistics;
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
        Task<bool> ExistsByIdAsync(string animalId);

        Task<AnimalDetailsViewModel?> GetDetailsByIdAsync(string animalId);

        Task<bool> IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(string animalId,string volunteerId);

        Task<AnimalFormModel> GetAnimalForEditByIdAsync(string animalId);

        Task EditAnimalByIdAndFormModelAsync(string animalId, AnimalFormModel formModel);
        Task<AnimalPreDeletedViewModel> GetAnimalForDeleteByIdAsync(string animalId);
        Task DeleteAnimalByIdAsync(string animalId);


        Task<AnimalAddCharacteristicViewModel> GetCharacteristicByIdAsync(string animalId);
        Task AddAnimalCharactericticByIdAsync(string animalId, AnimalAddCharacteristicViewModel model);

        Task<StatisticsServiceModel> GetStatisticsAsync();
    }
}
