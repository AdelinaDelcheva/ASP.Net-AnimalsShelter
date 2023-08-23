using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Web.Data;
using AnimalsShelterSystem.Web.ViewModels.Animal;
using AnimalsShelterSystem.Web.ViewModels.Care;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Services.Data
{
    public class CareService : ICareService
    {
        private readonly AnimalsShelterDbContext dbContext;
        public CareService(AnimalsShelterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> ExistByIdAsync(int id)
        {
            return await dbContext.Cares.AnyAsync(c=>c.Id==id);
        }

        public async Task<IEnumerable<AnimalForShoppingCartViewModel>> GetAllCaresAsync()
        {
            var cares = await dbContext.Cares.Select(c => new CareAllViewModel()
            {
                Id = c.Id,
                Name = c.CareTypes.ToString(),
                Price = c.Price,

            }).ToListAsync();
            var allAnimals = await dbContext.Animals.Where(a=>a.IsDeleted==false && !a.AnimalAdopterId.HasValue).Select(a => new AnimalForShoppingCartViewModel()
            {
                Id = a.Id.ToString(),
                Name = a.Name,
                Age=a.Age,
                Breed=a.Breed.Breed,
                ImageUrl = a.ImageUrl,
                AllCares=cares
            }).ToListAsync();
           

            return allAnimals;
        }

        public async  Task<CareDetailsViewModel> GetCareDetailsAsync(int id)
        {
            return await this.dbContext.Cares
                 .Select(c => new CareDetailsViewModel() 
                 {
                     Id=c.Id,
                     Name=c.CareTypes.ToString(),
                     Description=c.Description
                 }).FirstAsync(c=>c.Id==id);
                 

        }
    }
}
