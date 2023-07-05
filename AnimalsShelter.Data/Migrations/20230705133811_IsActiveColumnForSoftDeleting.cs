using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterSystem.Data.Migrations
{
    public partial class IsActiveColumnForSoftDeleting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("0a449516-cf69-40c1-a59c-a71fbfc29b9a"));

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("5110cb7f-8e41-47f2-8b39-639017355d88"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Animals",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "ImageUrl", "IsActive", "Name" },
                values: new object[] { new Guid("068030d2-1cfc-4d4b-a5d4-bea6c4174b8f"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", false, "Suzi" });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "ImageUrl", "IsActive", "Name" },
                values: new object[] { new Guid("46dd5968-e410-4d6c-bcde-a22bd82adc8a"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", false, "Medovina" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("068030d2-1cfc-4d4b-a5d4-bea6c4174b8f"));

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("46dd5968-e410-4d6c-bcde-a22bd82adc8a"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Animals");

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "CreatedOn", "ImageUrl", "Name" },
                values: new object[] { new Guid("0a449516-cf69-40c1-a59c-a71fbfc29b9a"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", "Suzi" });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "CreatedOn", "ImageUrl", "Name" },
                values: new object[] { new Guid("5110cb7f-8e41-47f2-8b39-639017355d88"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", "Medovina" });
        }
    }
}
