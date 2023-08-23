


namespace AnimalsShelterSystem.Data.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	using static AnimalsShelterSystem.Common.EntityValidationConstants.Order;

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

		[Required]
		[StringLength(CountryMaxLength)]
		public string Country { get; set; } = null!;

		[Required]
		[StringLength(CityMaxLength)]
		public string City { get; set; } = null!;

		[Required]
		[StringLength(StreetAddressMaxLength)]
		public string StreetAddress { get; set; } = null!;

		[StringLength(ZipCodeMaxLength)]
		public string? ZipCode { get; set; }

		
		[Required]
		[StringLength(PhoneNumberMaxLength)]
		public string PhoneNumber { get; set; } = null!;

		//  public OrderStatus Status { get; set; }


		[Required]
		[MaxLength(FirstNameMaxLength)]
		public string FirstName { get; set; } = null!;

		[Required]
		[MaxLength(LastNameMaxLength)]
		public string LastName { get; set; } = null!;


		public DateTime CreationDate { get; set; }

        public ICollection<OrderedCare> Orders { get; set; }=new HashSet<OrderedCare>();
    }
}
