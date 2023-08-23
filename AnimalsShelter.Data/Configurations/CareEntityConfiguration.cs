
namespace AnimalsShelterSystem.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using Models;
	public class CareEntityConfiguration : IEntityTypeConfiguration<Care>
	{
		public void Configure(EntityTypeBuilder<Care> builder)
		{
			builder.HasData(this.GenerateCares());
		}
		private Care[] GenerateCares()
		{
			ICollection<Care> cares = new HashSet<Care>();

			Care care;

			care = new Care()
			{
				Id=1,
				CareTypes = (Models.Enums.CareTypes.Feeding),
				Description="Great meal for special pet! Delicious food will make happy one of our pets, thank you!",
				Price=5.15m
			};
			cares.Add(care);

			care = new Care()
			{
				Id=2,
				CareTypes = (Models.Enums.CareTypes.Walking),
				Description = "Take for a walk one of our fantastic pets. They are friendly and playful, waiting you to spend more special time!",
				Price = 1
			};
			cares.Add(care);



			return cares.ToArray();
		}
	}
}
