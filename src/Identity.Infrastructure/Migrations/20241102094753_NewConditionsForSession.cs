using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewConditionsForSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AspNetUsers_IdentityId",
                schema: "public",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                schema: "public",
                table: "Sessions");

            migrationBuilder.RenameTable(
                name: "Sessions",
                schema: "public",
                newName: "sessions",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_IdentityId",
                schema: "public",
                table: "sessions",
                newName: "IX_sessions_IdentityId");

            migrationBuilder.AlterColumn<string>(
                name: "UserAgent",
                schema: "public",
                table: "sessions",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "public",
                table: "sessions",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                schema: "public",
                table: "sessions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "sessions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sessions",
                schema: "public",
                table: "sessions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_AspNetUsers_IdentityId",
                schema: "public",
                table: "sessions",
                column: "IdentityId",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sessions_AspNetUsers_IdentityId",
                schema: "public",
                table: "sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sessions",
                schema: "public",
                table: "sessions");

            migrationBuilder.RenameTable(
                name: "sessions",
                schema: "public",
                newName: "Sessions",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_sessions_IdentityId",
                schema: "public",
                table: "Sessions",
                newName: "IX_Sessions_IdentityId");

            migrationBuilder.AlterColumn<string>(
                name: "UserAgent",
                schema: "public",
                table: "Sessions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "public",
                table: "Sessions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                schema: "public",
                table: "Sessions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "Sessions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                schema: "public",
                table: "Sessions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AspNetUsers_IdentityId",
                schema: "public",
                table: "Sessions",
                column: "IdentityId",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
