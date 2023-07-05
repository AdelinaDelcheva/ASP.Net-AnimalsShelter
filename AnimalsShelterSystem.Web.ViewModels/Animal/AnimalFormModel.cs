


namespace AnimalsShelterSystem.Web.ViewModels.Animal
{
    using AnimalsShelterSystem.Web.ViewModels.AnimalBreed;
    using System.ComponentModel.DataAnnotations;

    using static AnimalsShelterSystem.Common.EntityValidationConstants.Animal;
    public class AnimalFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Range(AgeMinLength, AgeMaxLength)]
        public int Age { get; set; }


        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Breed")]
        public int BreedId { get; set; }
        public IEnumerable<AnimalBreedFormModel> Breeds { get; set; } = new HashSet<AnimalBreedFormModel>();



    }
}
