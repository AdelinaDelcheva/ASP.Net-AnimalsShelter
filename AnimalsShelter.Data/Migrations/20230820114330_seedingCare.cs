using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterSystem.Data.Migrations
{
    public partial class seedingCare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("41213d62-39e4-462a-a30d-ce438a3929cd"));

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("dae70393-0b6b-470d-b4c0-47315838e069"));

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("870efe2d-09af-4e24-a10d-f1ebda4a7323"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", "Suzi" },
                    { new Guid("f4f31387-ae8f-4074-9c03-7d2db7bad385"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", "Medovina" }
                });

            migrationBuilder.InsertData(
                table: "Cares",
                columns: new[] { "Id", "CareTypes", "Description", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Great meal for special pet! Delicious food will make happy one of our pets, thank you!", 5.15m },
                    { 2, 3, "Take for a walk one of our fantastic pets. They are friendly and playful, waiting you to spend more special time!", 1m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("870efe2d-09af-4e24-a10d-f1ebda4a7323"));

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: new Guid("f4f31387-ae8f-4074-9c03-7d2db7bad385"));

            migrationBuilder.DeleteData(
                table: "Cares",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cares",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "CreatedOn", "ImageUrl", "IsDeleted", "Name" },
                values: new object[] { new Guid("41213d62-39e4-462a-a30d-ce438a3929cd"), 4, null, new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Persialainen.jpg/330px-Persialainen.jpg", false, "Suzi" });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalAdopterId", "AnimalCareVolunteerId", "BreedId", "CreatedOn", "ImageUrl", "IsDeleted", "Name" },
                values: new object[] { new Guid("dae70393-0b6b-470d-b4c0-47315838e069"), 2, new Guid("a9bb84c2-3c92-4463-8d8b-fe7712553255"), new Guid("9c92331f-7bad-456f-871d-088b8b0df5fb"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.k9rl.com/wp-content/uploads/2015/12/Black-Pomeranian.jpg", false, "Medovina" });
        }
    }
}
