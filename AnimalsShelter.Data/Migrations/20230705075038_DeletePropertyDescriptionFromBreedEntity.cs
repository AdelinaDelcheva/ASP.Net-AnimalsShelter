using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterSystem.Data.Migrations
{
    public partial class DeletePropertyDescriptionFromBreedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("a71cf232-719e-43ac-81c2-a3ffb5c0b273"));

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("ec299e1e-0411-4cd6-a8dd-13411559b72b"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Breeds");

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("0a449516-cf69-40c1-a59c-a71fbfc29b9a"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", "Suzi" },
                    { new Guid("5110cb7f-8e41-47f2-8b39-639017355d88"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", "Medovina" }
                });

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "Breed",
                value: "Dog Pomeranian");

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "Breed",
                value: "Cat Persian");

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 3,
                column: "Breed",
                value: "Parrot Ara");

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 4,
                column: "Breed",
                value: "Dog Huski");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("0a449516-cf69-40c1-a59c-a71fbfc29b9a"));

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("5110cb7f-8e41-47f2-8b39-639017355d88"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Breeds",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "CreatedOn", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("a71cf232-719e-43ac-81c2-a3ffb5c0b273"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", "Suzi" },
                    { new Guid("ec299e1e-0411-4cd6-a8dd-13411559b72b"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", "Medovina" }
                });

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Breed", "Description" },
                values: new object[] { "Dog", "Pomeranian" });

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Breed", "Description" },
                values: new object[] { "Cat", "Persian" });

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Breed", "Description" },
                values: new object[] { "Parrot", "Ara" });

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Breed", "Description" },
                values: new object[] { "Dog", "Huski" });
        }
    }
}
