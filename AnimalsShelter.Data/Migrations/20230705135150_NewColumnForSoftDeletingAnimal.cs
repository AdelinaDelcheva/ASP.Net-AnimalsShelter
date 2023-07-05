using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterSystem.Data.Migrations
{
    public partial class NewColumnForSoftDeletingAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Animals",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "ImageUrl", "Name" },
                values: new object[] { new Guid("16973de7-3e12-41d2-94fa-23b3fd120293"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", "Suzi" });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "ImageUrl", "Name" },
                values: new object[] { new Guid("5d87e089-3bb5-4f52-8aef-d42f5534b4c5"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", "Medovina" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("16973de7-3e12-41d2-94fa-23b3fd120293"));

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("5d87e089-3bb5-4f52-8aef-d42f5534b4c5"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Animals");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Animals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "CreatedOn", "ImageUrl", "IsActive", "Name" },
                values: new object[] { new Guid("068030d2-1cfc-4d4b-a5d4-bea6c4174b8f"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", false, "Suzi" });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "CreatedOn", "ImageUrl", "IsActive", "Name" },
                values: new object[] { new Guid("46dd5968-e410-4d6c-bcde-a22bd82adc8a"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", false, "Medovina" });
        }
    }
}
