

namespace AnimalsShelterSystem.Web.ViewModels.Characteristic
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Characteristic;
    public class CharacteristicFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; } = null!;
    }
}
