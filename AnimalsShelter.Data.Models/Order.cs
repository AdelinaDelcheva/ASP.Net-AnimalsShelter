


namespace AnimalsShelterSystem.Data.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {


        public Order()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }


        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; } = null!;

        //  public OrderStatus Status { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<OrderedCare> Orders { get; set; }=new HashSet<OrderedCare>();
    }
}
