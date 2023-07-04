namespace AnimalsShelterSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class CharacteristicEntityConfiguration : IEntityTypeConfiguration<Characteristic>
    {
        public void Configure(EntityTypeBuilder<Characteristic> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private Characteristic[] GenerateCategories()
        {
            ICollection<Characteristic> categories = new HashSet<Characteristic>();

            Characteristic category;

            category = new Characteristic()
            {
                Id = 1,
                Name = "Obedient"
            };
            categories.Add(category);

            category = new Characteristic()
            {
                Id = 2,
                Name = "Playful"
            };
            categories.Add(category);

            category = new Characteristic()
            {
                Id = 3,
                Name = "Sleepy"
            };
            categories.Add(category);
            category = new Characteristic()
            {
                Id = 4,
                Name = "Disobedient"
            };
            categories.Add(category);
            category = new Characteristic()
            {
                Id = 5,
                Name = "Easy to train"
            };
            categories.Add(category);
            category = new Characteristic()
            {
                Id = 6,
                Name = "Intelligent"
            };
            categories.Add(category);
            category = new Characteristic()
            {
                Id = 7,
                Name = "Lovable"
            };
            categories.Add(category);
            return categories.ToArray();
        }
    }
}
