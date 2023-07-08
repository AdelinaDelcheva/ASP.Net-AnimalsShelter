
using AnimalsShelterSystem.Web.ViewModels.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AnimalsShelterSystem.Web.ViewModels.Animal
{
    public class AnimalAddCharacteristicViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public int Age { get; set; }

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        public string Breed { get; set; } = null!;

        [Display(Name = "Characteristic")]
        public int CharacteristicId { get; set; }

        public IEnumerable<CharacteristicAddViewModel> AllCharacteristics { get; set; }=new HashSet<CharacteristicAddViewModel>();
    }
}
