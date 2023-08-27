

namespace AnimalsShelterSystem.Web.Infrastructure.Extensitions
{
    using System.Reflection;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;

	using AnimalsShelterSystem.Data.Models;
	using static AnimalsShelterSystem.Common.GeneralApplicationConstants;
	using AnimalsShelterSystem.Web.Infrastructure.Middlewares;

	public static class WebAppBuilderExtensions
	{
		public static void AddAppService(this IServiceCollection service,Type typeService)
		{
            Assembly? serviceAssembly = Assembly.GetAssembly(typeService);
            if(serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid type");
            }
            Type[] serviceTypes= serviceAssembly.GetTypes().Where(s=>s.Name.EndsWith("Service")&& !s.IsInterface)
                .ToArray();

            foreach(var type in serviceTypes)
            {
                Type? interfaceType = type.GetInterface($"I{type.Name}");
                if(interfaceType==null)
                {
                    throw new InvalidOperationException("Invalid interface");
                }
                service.AddScoped(interfaceType, type);
            }
          
        }

		public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
		{
			using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

			IServiceProvider serviceProvider = scopedServices.ServiceProvider;

			UserManager<ApplicationUser> userManager =
				serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			RoleManager<IdentityRole<Guid>> roleManager =
				serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

			Task.Run(async () =>
			{
				if (await roleManager.RoleExistsAsync(AdminRoleName))
				{
					return;
				}

				IdentityRole<Guid> role =
					new IdentityRole<Guid>(AdminRoleName);

				await roleManager.CreateAsync(role);

				ApplicationUser adminUser =
					await userManager.FindByEmailAsync(email);

				await userManager.AddToRoleAsync(adminUser, AdminRoleName);
			})
			.GetAwaiter()
			.GetResult();

			return app;
		}

        public static IApplicationBuilder EnableOnlineUsersCheck(this IApplicationBuilder app)
        {
            return app.UseMiddleware<OnlineUsersMiddleware>();
        }

    }
}
