using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.Seller.Companies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImplementNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "permissions",
                newName: "path");

            migrationBuilder.RenameIndex(
                name: "ix_permissions_type_subscription_id",
                table: "permissions",
                newName: "ix_permissions_path_subscription_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "path",
                table: "permissions",
                newName: "type");

            migrationBuilder.RenameIndex(
                name: "ix_permissions_path_subscription_id",
                table: "permissions",
                newName: "ix_permissions_type_subscription_id");
        }
    }
}
