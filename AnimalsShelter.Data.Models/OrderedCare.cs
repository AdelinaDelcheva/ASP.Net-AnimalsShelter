

namespace AnimalsShelterSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;


    public class OrderedCare
    {


        public OrderedCare()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public int CareId { get; set; }
        public virtual Care Care { get; set; } = null!;


        public Guid AnimalId { get; set; }
        public virtual Animal Animal { get; set; } = null!;
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
