using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.Seller.Api.Companies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePermissionIndexTypeSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_permissions_Type_Status_SubscriptionId",
                table: "permissions");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_Type_SubscriptionId",
                table: "permissions",
                columns: new[] { "Type", "SubscriptionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_permissions_Type_SubscriptionId",
                table: "permissions");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_Type_Status_SubscriptionId",
                table: "permissions",
                columns: new[] { "Type", "Status", "SubscriptionId" },
                unique: true);
        }
    }
}
