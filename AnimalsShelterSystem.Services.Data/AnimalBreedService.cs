
namespace AnimalsShelterSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Web.Data;
    using AnimalsShelterSystem.Web.ViewModels.AnimalBreed;

    public class AnimalBreedService : IAnimalBreedService
    {
        private readonly AnimalsShelterDbContext dbContext;
        public AnimalBreedService(AnimalsShelterDbContext dbContext)
        {
            this.dbContext = dbContext;
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
