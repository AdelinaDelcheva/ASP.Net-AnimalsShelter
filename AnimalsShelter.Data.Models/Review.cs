



namespace AnimalsShelterSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static AnimalsShelterSystem.Common.EntityValidationConstants.Review;
    public class Review
    {
       

        [Key]
        public int Id { get; set; }
                    
        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; } = null!;


        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; } 
        public virtual ApplicationUser Creator { get; set; } = null!;


        [ForeignKey(nameof(Animal))]
        public Guid AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;

        public DateTime LastModified { get; set;}
    }
}
