
namespace AnimalsShelterSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static AnimalsShelterSystem.Common.EntityValidationConstants.Characteristic;
    public class Characteristic
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<AnimalCharacteristics> Animals { get; set; }=new HashSet<AnimalCharacteristics>();
    }
}
