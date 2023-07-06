


namespace AnimalsShelterSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AnimalsShelterSystem.Data.Models;
    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Services.Data.Models.Animal;
    using AnimalsShelterSystem.Web.Data;
    using AnimalsShelterSystem.Web.ViewModels.Animal;
    using AnimalsShelterSystem.Web.ViewModels.Animal.Enums;
    using AnimalsShelterSystem.Web.ViewModels.Volunteeer;
    using AnimalsShelterSystem.Web.ViewModels.Home;
   

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
                                 .Where(a=>a.IsDeleted==false)
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
                              .Where(a =>a.IsDeleted==false && a.AnimalCareVolunteerId.ToString() == volunteerId)
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

        public async Task<bool> ExistsByIdAsync(string animalId)
        {
            return await this.dbContext.Animals.AnyAsync(a => a.Id.ToString() == animalId);
        }

        public async Task<AnimalDetailsViewModel?> GetDetailsByIdAsync(string animalId)
        {
           

            var animal = await this.dbContext.Animals.Where(a=>a.IsDeleted==false && a.Id.ToString() == animalId)
                .Select(a => new AnimalDetailsViewModel()
                {
                    Id=animalId,
                    Name=a.Name,
                    Age=a.Age,
                    ImageUrl=a.ImageUrl,
                    Breed=a.Breed.Breed,
                    IsAdopted=a.AnimalAdopterId.HasValue,
                    VolunteerInfo=new VolunteerInfoOnAnimalViewModel()
                    {
                        FirstName= a.AnimalCareVolunteer.FirstName,
                        LastName=a.AnimalCareVolunteer.LastName,
                        Email=a.AnimalCareVolunteer.User.Email,
                        PhoneNumber=a.AnimalCareVolunteer.PhoneNumber
                    },
                    Characteristics=a.Characteristics.Select(c=>c.Characteristic.Name).ToList()
                    
                }).FirstOrDefaultAsync();

            return animal;
        }

        public async Task<bool> IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(string animalId, string volunteerId)
        {
            return await this.dbContext
                 .Animals
                 .AnyAsync(a =>a.IsDeleted==false && a.Id.ToString() == animalId && a.AnimalCareVolunteerId.ToString() == volunteerId);
        }

        public async Task EditAnimalByIdAndFormModelAsync(string animaId, AnimalFormModel formModel)
        {
            Animal animal = await this.dbContext
                .Animals
                .Where(a => a.IsDeleted==false)
                .FirstAsync(a => a.Id.ToString() == animaId);

            animal.Name = formModel.Name;
            animal.Age = formModel.Age;

            animal.ImageUrl = formModel.ImageUrl;
           
            animal.BreedId = formModel.BreedId;

            await this.dbContext.SaveChangesAsync();
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

        public async Task<AnimalFormModel> GetAnimalForEditByIdAsync(string animalId)
        {
            var result = await this.dbContext
                .Animals
                .Where(a => a.IsDeleted == false && a.Id.ToString() == animalId)
                .Select(a => new AnimalFormModel()
                {
                    Name=a.Name,
                    ImageUrl=a.ImageUrl,
                    Age= a.Age,

                    BreedId=a.BreedId
                }).FirstAsync();

            return result;
        }
    }
}
