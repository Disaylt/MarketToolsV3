using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketToolsV3.FakeData.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    task_id = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tasks", x => x.task_id);
                });

            migrationBuilder.CreateTable(
                name: "cookies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    value = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    path = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    domain = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    task_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cookies", x => x.id);
                    table.ForeignKey(
                        name: "fk_cookies_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    path = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    tag = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    json_body = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    task_end_condition = table.Column<int>(type: "integer", nullable: false),
                    task_complete_condition = table.Column<int>(type: "integer", nullable: false),
                    number_of_executions = table.Column<int>(type: "integer", nullable: false),
                    num_successful = table.Column<int>(type: "integer", nullable: false),
                    num_failed = table.Column<int>(type: "integer", nullable: false),
                    sort_index = table.Column<int>(type: "integer", nullable: false),
                    timeout_before_run = table.Column<int>(type: "integer", nullable: false),
                    num_group = table.Column<int>(type: "integer", nullable: true),
                    state = table.Column<int>(type: "integer", nullable: false),
                    task_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tasks_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_tasks_details_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "responses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    data = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    status_code = table.Column<int>(type: "integer", nullable: false),
                    task_detail_id = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_responses", x => x.id);
                    table.ForeignKey(
                        name: "fk_responses_tasks_details_task_detail_id",
                        column: x => x.task_detail_id,
                        principalTable: "tasks_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cookies_task_id",
                table: "cookies",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "ix_responses_task_detail_id",
                table: "responses",
                column: "task_detail_id");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_details_task_id_sort_index",
                table: "tasks_details",
                columns: new[] { "task_id", "sort_index" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cookies");

            migrationBuilder.DropTable(
                name: "responses");

            migrationBuilder.DropTable(
                name: "tasks_details");

            migrationBuilder.DropTable(
                name: "tasks");
        }
    }
}
