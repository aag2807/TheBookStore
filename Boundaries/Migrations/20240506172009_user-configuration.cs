using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boundaries.Migrations
{
    /// <inheritdoc />
    public partial class userconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 6, 13, 20, 9, 861, DateTimeKind.Local).AddTicks(9550), new DateTime(2024, 5, 6, 13, 20, 9, 861, DateTimeKind.Local).AddTicks(9550) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 6, 13, 20, 9, 861, DateTimeKind.Local).AddTicks(9550), new DateTime(2024, 5, 6, 13, 20, 9, 861, DateTimeKind.Local).AddTicks(9550) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 6, 12, 44, 5, 850, DateTimeKind.Local).AddTicks(8260), new DateTime(2024, 5, 6, 12, 44, 5, 850, DateTimeKind.Local).AddTicks(8260) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 6, 12, 44, 5, 850, DateTimeKind.Local).AddTicks(8270), new DateTime(2024, 5, 6, 12, 44, 5, 850, DateTimeKind.Local).AddTicks(8270) });
        }
    }
}
