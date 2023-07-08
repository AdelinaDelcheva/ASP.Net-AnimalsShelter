namespace AnimalsShelterSystem.Web.ViewModels.Characteristic
{
    
    using System.ComponentModel.DataAnnotations;

    using static AnimalsShelterSystem.Common.EntityValidationConstants.Characteristic;
    public class CharacteristicAddViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
       
        public string Name { get; set; } = null!;

    }
}
