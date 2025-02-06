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
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "TasksDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Tag = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    JsonBody = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    TaskCompleteCondition = table.Column<int>(type: "integer", nullable: false),
                    NumberOfExecutions = table.Column<int>(type: "integer", nullable: false),
                    NumSuccessful = table.Column<int>(type: "integer", nullable: false),
                    NumFailed = table.Column<int>(type: "integer", nullable: false),
                    NumberInQueue = table.Column<int>(type: "integer", nullable: false),
                    TimeoutBeforeRun = table.Column<int>(type: "integer", nullable: false),
                    NumGroup = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    TaskId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TasksDetails_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    TaskDetailsId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responses_TasksDetails_TaskDetailsId",
                        column: x => x.TaskDetailsId,
                        principalTable: "TasksDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValueUseHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    StatusCode = table.Column<int>(type: "integer", nullable: false),
                    ResponseBodyId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "IX_Responses_TaskDetailsId",
                table: "Responses",
                column: "TaskDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TasksDetails_TaskId",
                table: "TasksDetails",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueUseHistories_ResponseBodyId",
                table: "ValueUseHistories",
                column: "ResponseBodyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValueUseHistories");

            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropTable(
                name: "TasksDetails");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
