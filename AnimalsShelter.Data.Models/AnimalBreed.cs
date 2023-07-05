namespace AnimalsShelterSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static AnimalsShelterSystem.Common.EntityValidationConstants.AnimalBreed;
    public class AnimalBreed
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(BreedMaxLength)]
        public string Breed { get; set; } = null!;


        //[Required]
        //[MaxLength(DescriptionMaxLength)]
        //public string Description { get; set; } = null!;

       


        public virtual ICollection<Animal> Animals { get; set; }=new HashSet<Animal>(); 
    }
}
