using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.Seller.Companies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "number_of_token_checks",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "state",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "state_updated",
                table: "companies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "companies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "number_of_token_checks",
                table: "companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "state",
                table: "companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "state_updated",
                table: "companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
