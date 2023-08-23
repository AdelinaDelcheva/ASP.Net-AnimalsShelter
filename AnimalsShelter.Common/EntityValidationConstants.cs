namespace AnimalsShelterSystem.Common
{
    public static class EntityValidationConstants
    {
      
        public static class Animal
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;

            public const int AgeMinLength = 0;
            public const int AgeMaxLength = 100;

           

            public const int ImageUrlMaxLength = 2048;

            public const string PricePerMonthMinValue = "0";
            public const string PricePerMonthMaxValue = "2000";
        }
        public static class AnimalBreed
        {
            public const int BreedMinLength = 2;
            public const int BreedMaxLength = 50;


            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 500;
        }
        public static class Care
        {
            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 500;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "2000";
        }
        public static class Characteristic
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }
        public static class Review
        {
            public const int TextMinLength = 2;
            public const int TextMaxLength = 550;
        }

        public static class Volunteer
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 150;

            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }

        public static class User
        {
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;

            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 15;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 15;

		
		}

		public static class Order
		{
			public const int PhoneNumberMinLength = 3;
			public const int PhoneNumberMaxLength = 15;

			public const int CityMinLength = 2;
			public const int CityMaxLength = 40;

			public const int CountryMinLength = 3;
			public const int CountryMaxLength = 40;

			public const int StreetAddressMinLength = 5;
			public const int StreetAddressMaxLength = 100;

			public const int ZipCodeMinLength = 3;
			public const int ZipCodeMaxLength = 8;

			public const int FirstNameMinLength = 1;
			public const int FirstNameMaxLength = 15;

			public const int LastNameMinLength = 1;
			public const int LastNameMaxLength = 15;


		}
	}
}
