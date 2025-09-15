using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Code_First_Task_NewsPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Age", "Email", "Name", "NationalId", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 35, "john@example.com", "John Smith", "A123456", "1234", "01011111111" },
                    { 2, 29, "sara@example.com", "Sara Ali", "B987654", "5678", "01022222222" }
                });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Political news and updates", "Politics" },
                    { 2, "Sports news and events", "Sports" },
                    { 3, "Latest technology trends", "Technology" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "AuthorId", "CatalogId", "Date", "Description", "Time", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateOnly(2025, 9, 15), "The results of the national election...", new TimeSpan(0, 14, 30, 0, 0), "Election Results" },
                    { 2, 2, 2, new DateOnly(2025, 9, 14), "Final match of the championship...", new TimeSpan(0, 20, 0, 0, 0), "Football Championship" },
                    { 3, 1, 3, new DateOnly(2025, 9, 13), "New AI model surpasses human performance...", new TimeSpan(0, 10, 0, 0, 0), "AI Breakthrough" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
