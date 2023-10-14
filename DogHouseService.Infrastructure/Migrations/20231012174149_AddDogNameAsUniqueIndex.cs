using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DogHouseService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDogNameAsUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Color", "Name", "TailLength", "Weight" },
                values: new object[,]
                {
                    { new Guid("5e4c9b73-aad8-42b6-b166-50c1cece9815"), "black&white", "Jessy", 7, 14 },
                    { new Guid("73c99b57-fb06-4bd6-a7d8-8d1d2939ad80"), "White", "Spot", 2, 20 },
                    { new Guid("ba11df00-f686-418c-906e-db8c530fe81b"), "red&amber", "Neo", 22, 32 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_Name",
                table: "Dogs",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dogs_Name",
                table: "Dogs");

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("5e4c9b73-aad8-42b6-b166-50c1cece9815"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("73c99b57-fb06-4bd6-a7d8-8d1d2939ad80"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("ba11df00-f686-418c-906e-db8c530fe81b"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
