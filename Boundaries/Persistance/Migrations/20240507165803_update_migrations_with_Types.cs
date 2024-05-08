using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boundaries.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class update_migrations_with_Types : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 7, 12, 58, 2, 952, DateTimeKind.Local).AddTicks(60), new DateTime(2024, 5, 7, 12, 58, 2, 952, DateTimeKind.Local).AddTicks(60) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 7, 12, 58, 2, 952, DateTimeKind.Local).AddTicks(60), new DateTime(2024, 5, 7, 12, 58, 2, 952, DateTimeKind.Local).AddTicks(60) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
