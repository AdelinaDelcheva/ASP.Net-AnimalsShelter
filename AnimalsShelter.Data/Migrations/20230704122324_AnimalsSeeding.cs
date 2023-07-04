using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterSystem.Data.Migrations
{
    public partial class AnimalsSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "ImageUrl", "Name" },
                values: new object[] { new Guid("a71cf232-719e-43ac-81c2-a3ffb5c0b273"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", "Suzi" });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "ImageUrl", "Name" },
                values: new object[] { new Guid("ec299e1e-0411-4cd6-a8dd-13411559b72b"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", "Medovina" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("a71cf232-719e-43ac-81c2-a3ffb5c0b273"));

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("ec299e1e-0411-4cd6-a8dd-13411559b72b"));
        }
    }
}
