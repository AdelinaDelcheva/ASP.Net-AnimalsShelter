


namespace AnimalsShelterSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using AnimalsShelterSystem.Data.Models;
    public class BreedEntityConfiguration : IEntityTypeConfiguration<AnimalBreed>
    {
        public void Configure(EntityTypeBuilder<AnimalBreed> builder)
        {
            builder.HasData(this.GenerateBreeds());
        }

        private AnimalBreed[] GenerateBreeds()
        {
            ICollection<AnimalBreed> breeds = new HashSet<AnimalBreed>();

            AnimalBreed breed;

            breed = new AnimalBreed()
            {
                Id= 1,
                Breed= "Dog Pomeranian"
               
                
            };
            breeds.Add(breed);

            breed = new AnimalBreed()
            {
                Id = 2,
                Breed = "Cat Persian"
               
            };
            breeds.Add(breed);
            breed = new AnimalBreed()
            {
                Id = 3,
                Breed = "Parrot Ara"
               
            };
            breeds.Add(breed);

            breed = new AnimalBreed()
            {
                Id = 4,
                Breed = "Dog Huski"
            };
            breeds.Add(breed);



            return breeds.ToArray();
        }
    }
}
