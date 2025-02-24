using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class DeploymentForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployment_item_tasks_deployment_items_deployment_item_id",
                table: "deployment_item_tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_items_deployments_deployment_id",
                table: "deployment_items");

            migrationBuilder.DropForeignKey(
                name: "FK_deployments_environments_environment_id",
                table: "deployments");

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_item_tasks_deployment_items_deployment_item_id",
                table: "deployment_item_tasks",
                column: "deployment_item_id",
                principalTable: "deployment_items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_items_deployments_deployment_id",
                table: "deployment_items",
                column: "deployment_id",
                principalTable: "deployments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_deployments_environments_environment_id",
                table: "deployments",
                column: "environment_id",
                principalTable: "environments",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployment_item_tasks_deployment_items_deployment_item_id",
                table: "deployment_item_tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_items_deployments_deployment_id",
                table: "deployment_items");

            migrationBuilder.DropForeignKey(
                name: "FK_deployments_environments_environment_id",
                table: "deployments");

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_item_tasks_deployment_items_deployment_item_id",
                table: "deployment_item_tasks",
                column: "deployment_item_id",
                principalTable: "deployment_items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_items_deployments_deployment_id",
                table: "deployment_items",
                column: "deployment_id",
                principalTable: "deployments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_deployments_environments_environment_id",
                table: "deployments",
                column: "environment_id",
                principalTable: "environments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
