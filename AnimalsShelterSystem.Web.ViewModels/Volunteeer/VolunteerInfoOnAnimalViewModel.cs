

namespace AnimalsShelterSystem.Web.ViewModels.Volunteeer
{

    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;
    public class VolunteerInfoOnAnimalViewModel
    {
        [Display(Name = "Fist Name")]
        public string FirstName { get; set; } = null!;
               

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
