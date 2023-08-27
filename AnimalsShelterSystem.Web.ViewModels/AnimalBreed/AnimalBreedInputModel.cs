
namespace AnimalsShelterSystem.Web.ViewModels.AnimalBreed
{
    using System.ComponentModel.DataAnnotations;

    using static AnimalsShelterSystem.Common.EntityValidationConstants.AnimalBreed;
    public class AnimalBreedInputModel
    {
        [Required]
        [StringLength(BreedMaxLength, MinimumLength = BreedMinLength)]
        public string Breed { get; set; } = null!;
    }
}
