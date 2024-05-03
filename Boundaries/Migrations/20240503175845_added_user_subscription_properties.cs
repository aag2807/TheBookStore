using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Boundaries.Migrations
{
    /// <inheritdoc />
    public partial class added_user_subscription_properties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubscribedToNewsLetter",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionDate",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UnsubscriptionDate",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "CreatedAt", "Email", "IsAdmin", "IsBlocked", "IsDeleted", "IsSubscribedToNewsLetter", "Password", "SubscriptionDate", "UnsubscriptionDate", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 3, 13, 58, 45, 185, DateTimeKind.Local).AddTicks(4000), "admin@email.com", true, false, false, false, "admin", null, null, new DateTime(2024, 5, 3, 13, 58, 45, 185, DateTimeKind.Local).AddTicks(4000), "admin" },
                    { 2, new DateTime(2024, 5, 3, 13, 58, 45, 185, DateTimeKind.Local).AddTicks(4010), "user@email.com", false, false, false, false, "user", null, null, new DateTime(2024, 5, 3, 13, 58, 45, 185, DateTimeKind.Local).AddTicks(4010), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "IsSubscribedToNewsLetter",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SubscriptionDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UnsubscriptionDate",
                table: "User");
        }
    }
}
