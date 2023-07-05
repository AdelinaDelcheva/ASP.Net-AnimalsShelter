


namespace AnimalsShelterSystem.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static AnimalsShelterSystem.Common.EntityValidationConstants.Animal;
    public class Animal
    {
        public Animal()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }


        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Range(AgeMinLength,AgeMaxLength)]
        public int Age { get; set; }

   
        public int BreedId { get; set; }
        public virtual  AnimalBreed Breed { get; set; } = null!;


        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

      
        public bool IsDeleted { get; set; }
      
        public Guid? AnimalAdopterId { get; set; }
        public virtual ApplicationUser? AnimalAdopter { get; set; }

        public Guid AnimalCareVolunteerId { get; set; } 
        public virtual Volunteer AnimalCareVolunteer { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<AnimalCharacteristics> Characteristics { get; set; }=new HashSet<AnimalCharacteristics>();
        
        public virtual ICollection<Review> Reviews { get; set; }=new HashSet<Review>();

      

    }
}