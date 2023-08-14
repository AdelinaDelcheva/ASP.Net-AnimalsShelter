
using AnimalsShelterSystem.Web.ViewModels.Characteristic.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace AnimalsShelterSystem.Web.ViewModels.Characteristic
{
    public class CharacteristicViewModel:ICharacteristicDetailsModel
    {
        public int Id { get; set; }

       

        public string Name { get; set; } = null!;
    }
}
