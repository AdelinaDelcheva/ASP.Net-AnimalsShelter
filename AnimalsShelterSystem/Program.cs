
namespace AnimalsShelterSystem.Web
{
    using Microsoft.EntityFrameworkCore;


    using AnimalsShelterSystem.Web.Data;
    using AnimalsShelterSystem.Data.Models;
    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Web.Infrastructure.Extensitions;
    using AnimalsShelterSystem.Web.Infrastructure.ModelBinders;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using AnimalsShelterSystem.Web.Infrastructure.Services.Interfaces;
    using AnimalsShelterSystem.Web.Infrastructure.Services;
    using static AnimalsShelterSystem.Common.GeneralApplicationConstants;
   
    using Ganss.Xss;
    using Microsoft.AspNetCore.Identity;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<AnimalsShelterDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount =
                    builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireLowercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireUppercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireNonAlphanumeric =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequiredLength =
                    builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            })
				 .AddRoles<IdentityRole<Guid>>()
				  .AddEntityFrameworkStores<AnimalsShelterDbContext>();
			
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

            builder.Services.AddSession();

           


			builder.Services.AddAppService(typeof(IAnimalService));
            builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
            builder.Services.AddScoped<IHtmlSanitizer, HtmlSanitizer>();

			builder.Services
                .AddControllersWithViews()
                .AddMvcOptions(opt =>
                {
                    opt.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                    opt.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

			if (app.Environment.IsDevelopment())
			{
				app.SeedAdministrator(DevelopmentAdminEmail);
			}

			//app.MapControllerRoute(
			//    name: "default",
			//    pattern: "{controller=Home}/{action=Index}/{id?}");
			//app.MapRazorPages();

			app.UseEndpoints(config =>
            {
                config.MapControllerRoute(
                    name: "areas",
                    pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                config.MapControllerRoute(
                    name: "ProtectingUrlRoute",
                    pattern: "/{controller}/{action}/{id}/{information}",
                    defaults: new { Controller = "Characteristic", Action = "Details" });

                config.MapDefaultControllerRoute();

                config.MapRazorPages();
            });

            app.Run();
        }
    }
}