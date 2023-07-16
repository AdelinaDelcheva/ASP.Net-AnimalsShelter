using AnimalsShelterSystem.Services.Data.Interfaces;
using AnimalsShelterSystem.Web.Data;
using AnimalsShelterSystem.Web.Infrastructure.Extensitions;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelterSystem.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


            builder.Services.AddDbContext<AnimalsShelterDbContext>(options =>
               options.UseSqlServer(connectionString));

            builder.Services.AddAppService(typeof(IAnimalService));

            builder.Services.AddControllers();
          
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("AnimalShelterSystem", policyBuilder =>
                {
                    policyBuilder
                        .WithOrigins("https://localhost:7076")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

           
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseCors("AnimalShelterSystem");
            app.Run();
        }
    }
}