using AnimalsShelterSystem.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Services.Data.Interfaces
{
	public interface IUserService
	{
		//Task<string> GetFullNameByEmailAsync(string email);

		//Task<string> GetFullNameByIdAsync(string userId);

		Task<IEnumerable<UserViewModel>> AllAsync();
	}
}
