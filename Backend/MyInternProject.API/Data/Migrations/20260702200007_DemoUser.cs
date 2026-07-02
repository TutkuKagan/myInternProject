using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInternProject.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class DemoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsActive", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 7, 2, 20, 0, 7, 559, DateTimeKind.Utc).AddTicks(2571), "deneme@gmail.com", "Tutku", true, "Bayir", "Deneme", new DateTime(2026, 7, 2, 20, 0, 7, 559, DateTimeKind.Utc).AddTicks(2571), "DemoPlayer1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
