using AnimalsShelterSystem.Web.ViewModels.Characteristic.Interfaces;


namespace AnimalsShelterSystem.Web.Infrastructure.Extensitions
{
    public static class ViewModelsExtensions
    {
        public static string GetUrlInformation(this ICharacteristicDetailsModel model)
        {
            return model.Name.Replace(" ", "-");
        }
    }
}
