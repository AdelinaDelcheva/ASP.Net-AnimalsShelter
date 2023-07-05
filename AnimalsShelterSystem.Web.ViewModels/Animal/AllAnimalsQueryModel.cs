

namespace AnimalsShelterSystem.Web.ViewModels.Animal
{
    using System.ComponentModel.DataAnnotations;

    using AnimalsShelterSystem.Web.ViewModels.Animal.Enums;
    using static AnimalsShelterSystem.Common.GeneralApplicationConstants;
    public class AllAnimalsQueryModel
    {
        public AllAnimalsQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.AnimalsPerPage = EntitiesPerPage;

            this.Breeds = new HashSet<string>();
            this.Animals = new HashSet<AnimalAllViewModel>();
        }

        public string? Breed { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Animals By")]
        public AnimalSorting AnimalSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Animals On Page")]
        public int AnimalsPerPage { get; set; }

        public int TotalAnimals { get; set; }

        public IEnumerable<string> Breeds { get; set; }

        public IEnumerable<AnimalAllViewModel> Animals { get; set; }
    }
}
