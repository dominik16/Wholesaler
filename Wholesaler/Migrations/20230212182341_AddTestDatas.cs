using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Wholesaler.Migrations
{
    /// <inheritdoc />
    public partial class AddTestDatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "Address", "City", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Graniczna 12", "Kraków", "Magazyn Wewnętrzny", "Detaliczny" },
                    { 2, "Wesoła 46", "Rzeszów", "Magazyn Zewnętrzny Zadaszony", "Hurtowy" },
                    { 3, "Słoneczna 2", "Gdańsk", "Magazyn Niezadaszony", "Hurtowy" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "StorageId", "Unit" },
                values: new object[,]
                {
                    { 1, "Kabel prądowy", "Kabel YDY", 99.989999999999995, 1, "m/b" },
                    { 2, "Żarówka do świecenia", "Żarówka 100W", 10.99, 1, "szt" },
                    { 3, "Śrubokręt krzyżakowy", "Śrubokręt", 5.0, 2, "szt" },
                    { 4, "Kabel wysokonapięciowy", "Kabel RC", 800.0, 2, "m/b" },
                    { 5, "Narzędzie - nóż", "Nóż do tapet", 1.99, 1, "szt" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
