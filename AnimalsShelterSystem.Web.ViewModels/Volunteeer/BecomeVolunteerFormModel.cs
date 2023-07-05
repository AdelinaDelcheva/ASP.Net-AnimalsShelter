
namespace AnimalsShelterSystem.Web.ViewModels.Volunteeer
{
    using System.ComponentModel.DataAnnotations;

    using static AnimalsShelterSystem.Common.EntityValidationConstants.Volunteer;

    public class BecomeVolunteerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
       
        [Display(Name = "Fist Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;


        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;

    }
}
