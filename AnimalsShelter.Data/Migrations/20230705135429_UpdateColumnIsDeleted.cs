using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterSystem.Data.Migrations
{
    public partial class UpdateColumnIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("16973de7-3e12-41d2-94fa-23b3fd120293"));

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("5d87e089-3bb5-4f52-8aef-d42f5534b4c5"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Animals",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "ImageUrl", "Name" },
                values: new object[] { new Guid("5c4b86e9-57eb-4d3b-8108-89c7ae7b5f1a"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", "Suzi" });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "ImageUrl", "Name" },
                values: new object[] { new Guid("8f825a9c-4628-49d1-a3fd-ed5b9e3c0b9b"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", "Medovina" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("5c4b86e9-57eb-4d3b-8108-89c7ae7b5f1a"));

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("8f825a9c-4628-49d1-a3fd-ed5b9e3c0b9b"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Animals",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "CreatedOn", "ImageUrl", "IsDeleted", "Name" },
                values: new object[] { new Guid("16973de7-3e12-41d2-94fa-23b3fd120293"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", false, "Suzi" });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "CreatedOn", "ImageUrl", "IsDeleted", "Name" },
                values: new object[] { new Guid("5d87e089-3bb5-4f52-8aef-d42f5534b4c5"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", false, "Medovina" });
        }
    }
}
