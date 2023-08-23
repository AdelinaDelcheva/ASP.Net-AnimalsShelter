using AnimalsShelterSystem.Web.ViewModels.Care;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnimalsShelterSystem.Web.ViewModels.Animal
{
    public class AnimalForShoppingCartViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string Breed { get; set; } = null!;
        public int Age { get; set; }

		[Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        public ICollection<CareAllViewModel> AllCares { get; set; }=new HashSet<CareAllViewModel>();
    }
}
