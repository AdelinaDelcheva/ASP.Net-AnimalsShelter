using AnimalsShelterSystem.Web.ViewModels.Animal;
using AnimalsShelterSystem.Web.ViewModels.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Services.Data.Interfaces
{
    public interface ICareService
    {
        Task<bool> ExistByIdAsync(int id);

        Task<IEnumerable<AnimalForShoppingCartViewModel>> GetAllCaresAsync();

        Task<CareDetailsViewModel> GetCareDetailsAsync(int id);

    }
}
