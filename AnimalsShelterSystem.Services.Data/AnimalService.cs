
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
    using AnimalsShelterSystem.Services.Data.Models.Statistics;
    using AnimalsShelterSystem.Web.ViewModels.Characteristic;
    using AnimalsShelterSystem.Services.Data.Models.ShoppingCart;
    using System.Security;

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
            return await this.dbContext
                .Animals
                .AnyAsync(a =>a.IsDeleted==false && a.Id.ToString() == animalId);
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
                    Characteristics=a.Characteristics.Select(c=>new CharacteristicViewModel(){
                        Id=c.Characteristic.Id,
                        Name=c.Characteristic.Name
                    }).ToList(),
                    Reviews=a.Reviews.Select(r=>new Web.ViewModels.Review.ReviewViewModel()
                    {
                        Id=r.Id,
                        Text=r.Text,
                        CreatedOn=r.LastModified.ToString("yyyy-MM-dd H:mm"),
                        Creator=r.Creator.UserName
                    })
                    
                }).FirstOrDefaultAsync();

            return animal;
        }

        public async Task<bool> IsVolunteerWithIdCaretakeOfAnimalWithIdAsync(string animalId, string volunteerId)
        {
            return await this.dbContext
                 .Animals
                 .AnyAsync(a =>a.IsDeleted==false && a.Id.ToString() == animalId && a.AnimalCareVolunteerId.ToString() == volunteerId);
        }

        public async Task EditAnimalByIdAndFormModelAsync(string animalId, AnimalFormModel formModel)
        {
            Animal animal = await this.dbContext
                .Animals
                .Where(a => a.IsDeleted==false)
                .FirstAsync(a => a.Id.ToString() == animalId);

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

        public async Task<AnimalPreDeletedViewModel> GetAnimalForDeleteByIdAsync(string animalId)
        {
            Animal animal = await this.dbContext
               .Animals
               .Include(a=>a.Breed)
               .Where(a=>a.IsDeleted==false)
               .FirstAsync(a => a.Id.ToString() == animalId);

            return new AnimalPreDeletedViewModel
            {
                Name=animal.Name,
                Breed=animal.Breed.Breed,
                ImageUrl = animal.ImageUrl
            };
        }

        public async Task DeleteAnimalByIdAsync(string animalId)
        {
            Animal animalToDelete = await this.dbContext
                .Animals
                .Where(h => h.IsDeleted==false)
                .FirstAsync(h => h.Id.ToString() == animalId);

            animalToDelete.IsDeleted = true;

            await this.dbContext.SaveChangesAsync();
        }

        

        public async Task AddAnimalCharactericticByIdAsync(string animalId, AnimalAddCharacteristicViewModel model)
        {
            Animal animal = await this.dbContext
                   .Animals
                   .Include(a => a.Characteristics)
                   .Where(a => a.IsDeleted == false)
            .FirstAsync(a => a.Id.ToString() == animalId);

         



            animal.Characteristics.Add(new AnimalCharacteristics()
            {
                CharacteristicId = model.CharacteristicId,
                AnimalId = animal.Id
            });



            await this.dbContext.SaveChangesAsync();
        }

        public async Task<AnimalAddCharacteristicViewModel> GetCharacteristicByIdAsync(string animalId)
        {
            var animal = await this.dbContext.Animals.Where(a => a.IsDeleted == false && a.Id.ToString() == animalId)
                .Select(a => new AnimalAddCharacteristicViewModel()
                {
                    Id = animalId,
                    Name = a.Name,
                    ImageUrl=a.ImageUrl,
                    Breed=a.Breed.Breed
              
                }).FirstOrDefaultAsync();

            return animal;
        }

        public async Task<StatisticsServiceModel> GetStatisticsAsync()
        {
            return new StatisticsServiceModel()
            {
                TotalAnimals = await dbContext.Animals.Where(a => a.IsDeleted == false).CountAsync(),
                TotalAdoptions = await dbContext.Animals.Where(a => a.AnimalAdopterId.HasValue).CountAsync()
            };

        }

        public async Task RemoveAnimalCharactericticByIdAsync(int id, string animalId)
        {
            var exist = await this.dbContext.AnimalsCharacteristics
                .FirstOrDefaultAsync(a => a.AnimalId.ToString() == animalId && a.CharacteristicId == id);
            if (exist!=null)
            {
                AnimalCharacteristics curent = exist;
                this.dbContext.AnimalsCharacteristics.Remove(curent);
                await this.dbContext.SaveChangesAsync();
            }
        }

		public async Task<IList<ProductForCartDto>> GetProductsForCart(IList<ShoppingCartItem> cartItems)
		{
            
            IList<ProductForCartDto> result=new List<ProductForCartDto>();
            foreach(var item in cartItems)
            {
                var curent = await dbContext
                    
                    .Animals
                    .Where(a => a.Id.ToString() == item.Id && !a.IsDeleted)
                    .Select(a=> new ProductForCartDto()
					{
						Id = a.Id.ToString(),
						Name = a.Name,
						Image = a.ImageUrl,
						Breed = a.Breed.Breed,
						Age = a.Age,
						CareId = item.CareId,


					})
                    .FirstOrDefaultAsync();
                if (curent!=null)
                {
					var currenCare = await dbContext.Cares.FirstAsync(c => c.Id == curent.CareId);
					curent.CareName = currenCare.CareTypes.ToString();
					curent.Price = currenCare.Price;

					result.Add(curent);

                }
            }
            
           
           
            

            return result;
        }

        public async Task<bool> IsAdoptedAsync(string id)
        {
            Animal animal = await this.dbContext.Animals.FirstAsync(a => a.Id.ToString() == id);
            return animal.AnimalAdopterId.HasValue;
        }

        public async Task AdoptAnimalAsync(string animalId, string user)
        {
            Animal animal = await this.dbContext.Animals.FirstAsync(a => a.Id.ToString() == animalId);
            animal.AnimalAdopterId=Guid.Parse(user);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
