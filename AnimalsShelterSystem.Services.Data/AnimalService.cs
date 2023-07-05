


namespace AnimalsShelterSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AnimalsShelterSystem.Data.Models;
    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Services.Data.Models.Animal;
    using AnimalsShelterSystem.Web.Data;
    using AnimalsShelterSystem.Web.ViewModels.Animal;
    using AnimalsShelterSystem.Web.ViewModels.Animal.Enums;
    using AnimalsShelterSystem.Web.ViewModels.Home;
    using Microsoft.EntityFrameworkCore;

    public class AnimalService : IAnimalService
    {
        private readonly AnimalsShelterDbContext dbContext;
        public AnimalService(AnimalsShelterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllAnimalFilteredAndPagedServiceModel> AllAsync(AllAnimalsQueryModel model)
        {
             IQueryable<Animal> animalsQuery=this.dbContext
                .Animals
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(model.Breed))
            {
                animalsQuery = animalsQuery
                    .Where(a => a.Breed.Breed == model.Breed);
            }

            if (!string.IsNullOrWhiteSpace(model.SearchString))
            {
                string wildCard = $"%{model.SearchString.ToLower()}%";

                animalsQuery = animalsQuery
                    .Where(a => EF.Functions.Like(a.Name, wildCard) ||
                                EF.Functions.Like(a.Breed.Breed, wildCard));
                               
            }

            animalsQuery = model.AnimalSorting switch
            {
                AnimalSorting.Newest => animalsQuery
                    .OrderByDescending(h => h.CreatedOn),
                AnimalSorting.Oldest => animalsQuery
                    .OrderBy(h => h.CreatedOn),
               
                _ => animalsQuery
                    .OrderBy(a => a.AnimalAdopterId != null)
                    .ThenByDescending(h => h.CreatedOn)
            };

            IEnumerable<AnimalAllViewModel> allAnimals = await animalsQuery
                .Where(a=>a.IsDeleted==false)
                .Skip((model.CurrentPage - 1) * model.AnimalsPerPage)
                .Take(model.AnimalsPerPage)
                .Select(a => new AnimalAllViewModel()
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                    Breed = a.Breed.Breed,
                    ImageUrl = a.ImageUrl,
                   
                    IsAdopted = a.AnimalAdopterId.HasValue
                })
                .ToArrayAsync();
            int totalAnimals = animalsQuery.Count();

            return new AllAnimalFilteredAndPagedServiceModel()
            {
                TotalAnimalsCount = totalAnimals,
                Animals = allAnimals
            };
        }

        public async Task<IEnumerable<AnimalAllViewModel>> AllByUserIdAsync(string userId)
        {
            var result = await this.dbContext
                                 .Animals
                                 .Where(a => a.AnimalAdopterId.HasValue && a.AnimalAdopterId.ToString() == userId)
                                 .Select(a => new AnimalAllViewModel()
                                 {
                                     Id = a.Id.ToString(),
                                     Name = a.Name,
                                     Breed = a.Breed.Breed,
                                     ImageUrl = a.ImageUrl,

                                     IsAdopted = a.AnimalAdopterId.HasValue
                                 })
                                 .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<AnimalAllViewModel>> AllByVolunteerIdAsync(string volunteerId)
        {
            var result = await this.dbContext
                              .Animals
                              .Where(a => a.AnimalCareVolunteerId.ToString() == volunteerId)
                              .Select(a => new AnimalAllViewModel()
                              {
                                  Id = a.Id.ToString(),
                                  Name = a.Name,
                                  Breed = a.Breed.Breed,
                                  ImageUrl = a.ImageUrl,

                                  IsAdopted = a.AnimalAdopterId.HasValue
                              })
                              .ToListAsync();

            return result;
        }

        public async Task<string> CreateAndReturnIdAsync(AnimalFormModel model, string volunteerId)
        {
            Animal newAnimal = new Animal()
            {
                Name = model.Name,
                Age = model.Age,
                BreedId = model.BreedId,
                ImageUrl = model.ImageUrl,
                AnimalCareVolunteerId = Guid.Parse(volunteerId),
               
            };
            await this.dbContext.Animals.AddAsync(newAnimal);
            await this.dbContext.SaveChangesAsync();

            return newAnimal.Id.ToString();
        }

        public async Task<IEnumerable<IndexViewModel>> LastThreeAnimals()
        {
            var result = await dbContext.Animals
                 .Where(a=>a.IsDeleted==false)
                 .OrderByDescending(a => a.CreatedOn)
                 .Take(3)
                 .Select(a => new IndexViewModel()
                 {
                     Id = a.Id.ToString(),
                     Name = a.Name,
                     ImageUrl = a.ImageUrl
                 })
                 .ToListAsync();

            return result;
        }
    }
}
