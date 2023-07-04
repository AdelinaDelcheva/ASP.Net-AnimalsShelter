


namespace AnimalsShelterSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Web.Data;
    using AnimalsShelterSystem.Web.ViewModels.Home;
    using Microsoft.EntityFrameworkCore;

    public class AnimalService : IAnimalService
    {
        private readonly AnimalsShelterDbContext dbContext;
        public AnimalService(AnimalsShelterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> LastThreeAnimals()
        {
            var result = await dbContext.Animals
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
