using AnimalsShelterSystem.Web.ViewModels.Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Services.Data.Models.Animal
{
    public class AllAnimalFilteredAndPagedServiceModel
    {
        public int TotalAnimalsCount { get; set; }

        public IEnumerable<AnimalAllViewModel> Animals { get; set; }=new HashSet<AnimalAllViewModel>();
    }
}
