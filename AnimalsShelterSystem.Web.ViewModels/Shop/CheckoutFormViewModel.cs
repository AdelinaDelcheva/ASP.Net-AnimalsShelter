
using System.ComponentModel.DataAnnotations;
using static AnimalsShelterSystem.Common.EntityValidationConstants.Order;

namespace AnimalsShelterSystem.Web.ViewModels.Shop
{
	public class CheckoutFormViewModel
	{
		public IList<CheckoutItemViewModel> Items { get; set; } = null!;

		[StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; } = null!;

		[StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
		[DataType(DataType.Text)]
		public string FirstName { get; set; } = null!;

		[StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
		[DataType(DataType.Text)]
		public string LastName { get; set; } = null!;

		//[DataType(DataType.EmailAddress)]
		//[EmailAddress]

		//public string? Email { get; set; }


		[StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
		[Required]

		public string Country { get; set; } = null!;

		[Required]
		[StringLength(CityMaxLength, MinimumLength = CityMinLength)]
		public string City { get; set; } = null!;

		[Required]
		[StringLength(StreetAddressMaxLength, MinimumLength = StreetAddressMinLength)]
		public string StreetAddress { get; set; } = null!;

		[StringLength(ZipCodeMaxLength, MinimumLength = ZipCodeMinLength)]
		public string? ZipCode { get; set; }
	}
}
