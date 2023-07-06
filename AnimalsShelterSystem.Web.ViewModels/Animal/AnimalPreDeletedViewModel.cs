

namespace AnimalsShelterSystem.Web.ViewModels.Animal
{
    using System.ComponentModel.DataAnnotations;

    public class AnimalPreDeletedViewModel
    {
        public string Name { get; set; } = null!;

        public string Breed { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
