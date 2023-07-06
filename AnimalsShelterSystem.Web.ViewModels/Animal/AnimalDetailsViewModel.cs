
using AnimalsShelterSystem.Web.ViewModels.Volunteeer;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AnimalsShelterSystem.Web.ViewModels.Animal
{
 
    public class AnimalDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public int Age { get; set; }

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        public string Breed { get; set; } = null!;
        [Display(Name = "Is Adopted")]
        public bool IsAdopted { get; set; }

        public VolunteerInfoOnAnimalViewModel VolunteerInfo { get; set; } = null!;
        public IEnumerable<string> Characteristics { get; set; } = new HashSet<string>();
    }
}
