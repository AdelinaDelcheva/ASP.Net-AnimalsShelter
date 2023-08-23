namespace AnimalsShelterSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class AnimalEntityConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            
            builder.Property(a=>a.CreatedOn).HasDefaultValueSql("GETDATE()");

            builder.Property(a => a.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(a=>a.AnimalCareVolunteer)
                   .WithMany(v=>v.AnimalsCare)
                   .HasForeignKey(a=>a.AnimalCareVolunteerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Breed)
                 .WithMany(v => v.Animals)
                 .HasForeignKey(a => a.BreedId)
                 .OnDelete(DeleteBehavior.NoAction);


           builder.HasData(this.GenerateAnimals());
        }

        private Animal[] GenerateAnimals()
        {
            ICollection<Animal> animals = new HashSet<Animal>();

            Animal animal;

            animal = new Animal()
            {
                Name = "Medovina",
                Age = 2,
                BreedId = 1,
                ImageUrl = "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg",
                AnimalAdopterId = Guid.Parse("A9BB84C2-3C92-4463-8D8B-FE7712553255"),
                AnimalCareVolunteerId = Guid.Parse("9C92331F-7BAD-456F-871D-088B8B0DF5FB")
            };
            animals.Add(animal);

            animal = new Animal()
            {
                Name = "Suzi",
                Age = 4,
                BreedId = 2,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg",
                AnimalCareVolunteerId = Guid.Parse("9C92331F-7BAD-456F-871D-088B8B0DF5FB")
            };
            animals.Add(animal);



            return animals.ToArray();
        }
    }
}
