using AnimalsShelterSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AnimalsShelterSystem.Web.Data
{
    public class AnimalsShelterDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AnimalsShelterDbContext(DbContextOptions<AnimalsShelterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; } = null!;
        public DbSet<AnimalBreed> Breeds { get; set; } = null!;
        public DbSet<Characteristic> Characteristics { get; set; } = null!;

        public DbSet<Care> Cares { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderedCare> OrderedCares { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Volunteer> Volunteers { get; set; } = null!;
        public DbSet<AnimalCharacteristics> AnimalsCharacteristics { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AnimalCharacteristics>().HasKey(k => new { k.CharacteristicId, k.AnimalId });

            builder.Entity<OrderedCare>().Property(p => p.Price).HasPrecision(18, 4);
            builder.Entity<Care>().Property(p => p.Price).HasPrecision(18, 4);

            Assembly configAssembly = Assembly.GetAssembly(typeof(AnimalsShelterDbContext)) ??
                                      Assembly.GetExecutingAssembly();


            builder.ApplyConfigurationsFromAssembly(configAssembly);


            base.OnModelCreating(builder);
        }
    }
}