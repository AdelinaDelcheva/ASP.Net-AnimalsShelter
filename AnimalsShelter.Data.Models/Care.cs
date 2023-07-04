



namespace AnimalsShelterSystem.Data.Models
{
   
    using System.ComponentModel.DataAnnotations;

    using AnimalsShelterSystem.Data.Models.Enums;
    using static AnimalsShelterSystem.Common.EntityValidationConstants.Care;

    public class Care
    {
        public int Id { get; set; }
       
      

        public  CareTypes CareTypes { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

       
      
    } 
}
