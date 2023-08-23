
namespace AnimalsShelterSystem.Services.Data.Interfaces
{
    using AnimalsShelterSystem.Web.ViewModels.AnimalBreed;
    using AnimalsShelterSystem.Web.ViewModels.Characteristic;

    public interface ICharacteristicService
    {
        Task<IEnumerable<CharacteristicAddViewModel>> AllCharacteristicsAsync();

        Task<IEnumerable<CharacteristicViewModel>> GetAllCharacteristicsAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<CharacteristicDetailsModel> GetDetailsByIdAsync(int id);

        Task<bool> AlreadyAddedByIdAsync(int id,string animalId);


    }
}
