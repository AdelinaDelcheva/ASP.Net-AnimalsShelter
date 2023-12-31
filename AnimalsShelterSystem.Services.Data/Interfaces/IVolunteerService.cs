﻿
namespace AnimalsShelterSystem.Services.Data.Interfaces
{
    using AnimalsShelterSystem.Web.ViewModels.Volunteeer;
    public interface IVolunteerService
    {
        Task<bool> VolunteerExistsByUserIdAsync(string userId);

        Task<bool> VolunteerExistsByPhoneNumberAsync(string phoneNumber);
        Task Create(string userId, BecomeVolunteerFormModel model);

        Task<string?> GetVolunteerIdByUserIdAsync(string userId);

        Task<bool> HasAnimalWithIdAsync(string? userId, string animalId);
    }
}
