using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DogHouseService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Color", "Name", "TailLength", "Weight" },
                values: new object[,]
                {
                    { new Guid("73706252-a827-44d0-b003-cab869c22ed4"), "White", "Spot", 2, 20 },
                    { new Guid("7c225151-512d-4ccb-9c7f-301bb6a99ce4"), "red&amber", "Neo", 22, 32 },
                    { new Guid("b008be48-ea56-4f5c-9118-ecca0462095e"), "black&white", "Jessy", 7, 14 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("73706252-a827-44d0-b003-cab869c22ed4"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("7c225151-512d-4ccb-9c7f-301bb6a99ce4"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("b008be48-ea56-4f5c-9118-ecca0462095e"));
        }
    }
}
