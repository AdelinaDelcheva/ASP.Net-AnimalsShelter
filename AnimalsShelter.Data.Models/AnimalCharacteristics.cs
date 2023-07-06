
namespace AnimalsShelterSystem.Data.Models
{
    public class AnimalCharacteristics
    {
       public Guid AnimalId { get; set; }
        public virtual Animal Animal { get; set; } = null!;

        public int CharacteristicId { get; set; }
        public virtual Characteristic Characteristic { get; set; } = null!;



    }
}
