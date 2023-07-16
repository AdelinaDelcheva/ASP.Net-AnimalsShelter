
namespace AnimalsShelterSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;


    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Web.Data;
    using AnimalsShelterSystem.Web.ViewModels.Volunteeer;
    using AnimalsShelterSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Internal;

    public class VolunteerService : IVolunteerService
    {
        private readonly AnimalsShelterDbContext dbContext;
        public VolunteerService(AnimalsShelterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> VolunteerExistsByPhoneNumberAsync(string phoneNumber)
        {
            bool result = await this.dbContext
             .Volunteers
             .AnyAsync(v => v.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task<bool> VolunteerExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
          .Volunteers
          .AnyAsync(v => v.UserId.ToString() == userId);

            return result;
        }


        public async Task Create(string userId, BecomeVolunteerFormModel model)
        {
            Volunteer newVolunteer = new Volunteer()
            {
                FirstName= model.FirstName,
                LastName=model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Volunteers.AddAsync(newVolunteer);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetVolunteerIdByUserIdAsync(string userId)
        {
          Volunteer? volunteer= await dbContext
                                        .Volunteers
                                        .FirstOrDefaultAsync(v=>v.UserId.ToString()==userId);
            if(volunteer==null)
            {
                return null;
            }

            return volunteer.Id.ToString();
        }

        public async Task<bool> HasAnimalWithIdAsync(string? userId, string animalId)
        {
           
            Volunteer? cur=await dbContext.Volunteers.Include(v=>v.AnimalsCare)
                .FirstOrDefaultAsync(v=>v.UserId.ToString()==userId);
           

            if (cur == null)
            {
                return false;
            }
            animalId = animalId.ToLower();
            return cur.AnimalsCare.Any(a=>a.Id.ToString()==animalId);

          //return await dbContext
          //      .Volunteers
          //      .AnyAsync(v=>v.UserId.ToString()==userId && v.AnimalsCare.Any(a=>a.Id.ToString()==animalId));
        }
    }
}
