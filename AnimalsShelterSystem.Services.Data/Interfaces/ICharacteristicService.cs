
namespace AnimalsShelterSystem.Services.Data.Interfaces
{
    using AnimalsShelterSystem.Web.ViewModels.AnimalBreed;
    using AnimalsShelterSystem.Web.ViewModels.Characteristic;

    public interface ICharacteristicService
    {
        Task<IEnumerable<CharacteristicAddViewModel>> AllCharacteristicsAsync();

        Task<bool> ExistsByIdAsync(int id);

    }
}
