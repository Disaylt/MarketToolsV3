using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.Seller.Companies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePermissionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_permissions_type_status_subscription_id",
                table: "permissions");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "permissions",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "ix_permissions_type_subscription_id",
                table: "permissions",
                columns: new[] { "type", "subscription_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_permissions_type_subscription_id",
                table: "permissions");

            migrationBuilder.AlterColumn<int>(
                name: "type",
                table: "permissions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "ix_permissions_type_status_subscription_id",
                table: "permissions",
                columns: new[] { "type", "status", "subscription_id" },
                unique: true);
        }
    }
}
