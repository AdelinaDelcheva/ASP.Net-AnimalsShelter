using AnimalsShelterSystem.Data.Models;
using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Web.Data;
using AnimalsShelterSystem.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Services.Data
{
	public class UserService : IUserService
	{
		private readonly AnimalsShelterDbContext dbContext;

		public UserService(AnimalsShelterDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<UserViewModel>> AllAsync()
		{
			var allUsers = await this.dbContext
			.Users
			.Select(u => new UserViewModel()
			{
				Id = u.Id.ToString(),
				Email = u.Email

			})
			.ToListAsync();
			foreach (UserViewModel user in allUsers)
			{
				Volunteer? volunteer = this.dbContext
					.Volunteers
					.FirstOrDefault(a => a.UserId.ToString() == user.Id);
				if (volunteer != null)
				{
					user.FullName = volunteer.FirstName + " " + volunteer.LastName;
					user.PhoneNumber = volunteer.PhoneNumber;
				}
				else
				{
					user.FullName = string.Empty;
					user.PhoneNumber = string.Empty;
				}
			}

			return allUsers;

		}
	}
}
