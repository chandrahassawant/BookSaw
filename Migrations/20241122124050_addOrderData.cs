using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BulkyWeb.Migrations
{
    /// <inheritdoc />
    public partial class addOrderData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateRegistered",
                value: new DateTime(2024, 11, 22, 18, 10, 49, 326, DateTimeKind.Local).AddTicks(5172));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateRegistered",
                value: new DateTime(2024, 11, 22, 18, 10, 49, 327, DateTimeKind.Local).AddTicks(5418));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedDate",
                table: "Products",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateRegistered",
                value: new DateTime(2024, 11, 22, 18, 0, 57, 579, DateTimeKind.Local).AddTicks(6205));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateRegistered",
                value: new DateTime(2024, 11, 22, 18, 0, 57, 580, DateTimeKind.Local).AddTicks(8256));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Author", "CategoryId", "CreatedDate", "Description", "ISBN", "ImageUrl", "IsAvailable", "ListPrice", "Price", "Price100", "Price50", "Stock", "Title" },
                values: new object[,]
                {
                    { 1, "Subhash Kak", 3, new DateOnly(1, 1, 1), "Eternal Bhārat describes India's civilizational engagement with the eternal...", "979-8885751537", "E:\\.NET\\BulkyWeb\\wwwroot\\product\\Eternal Bharat Truth, Meaning & Beauty\\front.jpg", true, 105.0, 95.0, 85.0, 90.0, 0, "Eternal Bharat: Truth, Meaning & Beauty" },
                    { 2, "Vikram Sampath", 3, new DateOnly(1, 1, 1), "As the intellectual fountainhead of the ideology of Hindutva...", "978-0670090303", "E:\\.NET\\BulkyWeb\\wwwroot\\product\\Savarkar Echoes from a Forgotten Past\\front.jpg", true, 45.0, 40.0, 30.0, 35.0, 0, "Savarkar: Echoes from a Forgotten Past" },
                    { 3, "Rajiv Malhotra", 1, new DateOnly(1, 1, 1), "India's integrity is being undermined by three global networks...", "B004WF4K5K", "E:\\.NET\\BulkyWeb\\wwwroot\\product\\Breaking India Western Interventions\\front.jpg", true, 60.0, 55.0, 45.0, 50.0, 0, "Breaking India: Western Interventions" }
                });
        }
    }
}
