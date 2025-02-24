using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class DeploymentStatusesForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "deployment_items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_deployment_items_status_id",
                table: "deployment_items",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_items_deployment_statuses_status_id",
                table: "deployment_items",
                column: "status_id",
                principalTable: "deployment_statuses",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployment_items_deployment_statuses_status_id",
                table: "deployment_items");

            migrationBuilder.DropIndex(
                name: "IX_deployment_items_status_id",
                table: "deployment_items");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "deployment_items");
        }
    }
}
