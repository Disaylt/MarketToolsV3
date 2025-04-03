using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOutBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_modules_identity_id_path_type_external_id",
                schema: "public",
                table: "modules");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "public",
                table: "modules");

            migrationBuilder.CreateTable(
                name: "integration_event_logs",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    time_sent = table.Column<int>(type: "integer", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    content = table.Column<string>(type: "character varying(99999)", maxLength: 99999, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_integration_event_logs", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_modules_identity_id_path_external_id",
                schema: "public",
                table: "modules",
                columns: new[] { "identity_id", "path", "external_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_integration_event_logs_transaction_id",
                schema: "public",
                table: "integration_event_logs",
                column: "transaction_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_integration_event_logs_transaction_id_state",
                schema: "public",
                table: "integration_event_logs",
                columns: new[] { "transaction_id", "state" });

            migrationBuilder.CreateIndex(
                name: "ix_integration_event_logs_type",
                schema: "public",
                table: "integration_event_logs",
                column: "type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "integration_event_logs",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "ix_modules_identity_id_path_external_id",
                schema: "public",
                table: "modules");

            migrationBuilder.AddColumn<string>(
                name: "type",
                schema: "public",
                table: "modules",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_modules_identity_id_path_type_external_id",
                schema: "public",
                table: "modules",
                columns: new[] { "identity_id", "path", "type", "external_id" },
                unique: true);
        }
    }
}
