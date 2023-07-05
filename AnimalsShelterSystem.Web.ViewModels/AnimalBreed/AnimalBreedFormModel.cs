
namespace AnimalsShelterSystem.Web.ViewModels.AnimalBreed
{
    using System.ComponentModel.DataAnnotations;

    using static AnimalsShelterSystem.Common.EntityValidationConstants.AnimalBreed;
    public class AnimalBreedFormModel
    {
        public int Id { get; set; }


        [Required]
        [StringLength(BreedMaxLength, MinimumLength = BreedMinLength)]
        public string Breed { get; set; } = null!;


       
    }
}
