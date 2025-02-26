using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketToolsV3.FakeData.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValueUseHistories");

            migrationBuilder.AddColumn<bool>(
                name: "Used",
                table: "Responses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Used",
                table: "Responses");

            migrationBuilder.CreateTable(
                name: "ValueUseHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ResponseBodyId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Path = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueUseHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueUseHistories_Responses_ResponseBodyId",
                        column: x => x.ResponseBodyId,
                        principalTable: "Responses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ValueUseHistories_ResponseBodyId",
                table: "ValueUseHistories",
                column: "ResponseBodyId");
        }
    }
}
