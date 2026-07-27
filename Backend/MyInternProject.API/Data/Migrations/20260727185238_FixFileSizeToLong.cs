using System;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInternProject.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixFileSizeToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Tasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "TaskAttachments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(BigInteger),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 27, 18, 52, 38, 581, DateTimeKind.Utc).AddTicks(5167), new DateTime(2026, 7, 27, 18, 52, 38, 581, DateTimeKind.Utc).AddTicks(5167) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<BigInteger>(
                name: "FileSize",
                table: "TaskAttachments",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 2, 20, 0, 7, 559, DateTimeKind.Utc).AddTicks(2571), new DateTime(2026, 7, 2, 20, 0, 7, 559, DateTimeKind.Utc).AddTicks(2571) });
        }
    }
}
