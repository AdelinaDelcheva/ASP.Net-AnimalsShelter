
namespace AnimalsShelterSystem.Services.Data
{
    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Web.Data;
    using AnimalsShelterSystem.Web.ViewModels.AnimalBreed;
    using AnimalsShelterSystem.Web.ViewModels.Characteristic;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CharacteristicService : ICharacteristicService
    {
        private readonly AnimalsShelterDbContext dbContext;
        public CharacteristicService(AnimalsShelterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CharacteristicAddViewModel>> AllCharacteristicsAsync()
        {
            IEnumerable<CharacteristicAddViewModel> allCharacteristics = await this.dbContext
                 .Characteristics
                 .AsNoTracking()
                 .Select(b => new CharacteristicAddViewModel()
                 {
                     Id = b.Id,
                     Name = b.Name
                 })
                 .ToArrayAsync();

            return allCharacteristics;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await dbContext.Characteristics.AnyAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<CharacteristicViewModel>> GetAllCharacteristicsAsync()
        {
            IEnumerable<CharacteristicViewModel> allCharacteristics = await this.dbContext
                .Characteristics
                .AsNoTracking()
                .Select(b => new CharacteristicViewModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToArrayAsync();

            return allCharacteristics;
        }

        public async  Task<CharacteristicDetailsModel> GetDetailsByIdAsync(int id)
        {
           return await this.dbContext.Characteristics.Select(c=>new CharacteristicDetailsModel()
           { 
               Id = c.Id,
               Name = c.Name
           }).FirstAsync(c=>c.Id==id);
        }
    }
}
