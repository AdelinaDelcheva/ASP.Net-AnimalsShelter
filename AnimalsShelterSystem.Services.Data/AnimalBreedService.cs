
namespace AnimalsShelterSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Web.Data;
    using AnimalsShelterSystem.Web.ViewModels.AnimalBreed;
    using AnimalsShelterSystem.Data.Models;

    public class AnimalBreedService : IAnimalBreedService
    {
        private readonly AnimalsShelterDbContext dbContext;
        public AnimalBreedService(AnimalsShelterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddBreedInDbAsync(AnimalBreedInputModel model)
        {
            AnimalBreed breed = new AnimalBreed()
            {
                Breed = model.Breed
            };

            await this.dbContext.Breeds.AddAsync(breed);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> AllBreedNamesAsync()
        {
            return await this.dbContext
                .Breeds
                .Select(b=>b.Breed)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<AnimalBreedFormModel>> AllBreedsAsync()
        {
            IEnumerable<AnimalBreedFormModel> allBreeds = await this.dbContext
                .Breeds
                .AsNoTracking()
                .Select(b => new AnimalBreedFormModel()
                {
                    Id = b.Id,
                    Breed=b.Breed
                })
                .ToArrayAsync();

            return allBreeds;
        }

       

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await dbContext.Breeds.AnyAsync(b => b.Id == id);
        }
    }
}
