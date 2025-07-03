using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WB.Seller.Companies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "normalized_login",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "subscription_codes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    subscription_entity_id = table.Column<int>(type: "integer", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscription_codes", x => x.id);
                    table.ForeignKey(
                        name: "fk_subscription_codes_subscriptions_subscription_entity_id",
                        column: x => x.subscription_entity_id,
                        principalTable: "subscriptions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_subscription_codes_subscription_entity_id",
                table: "subscription_codes",
                column: "subscription_entity_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subscription_codes");

            migrationBuilder.DropColumn(
                name: "normalized_login",
                table: "users");
        }
    }
}
