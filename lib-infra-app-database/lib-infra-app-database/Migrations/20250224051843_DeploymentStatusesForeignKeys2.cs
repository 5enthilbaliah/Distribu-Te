using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class DeploymentStatusesForeignKeys2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "deployment_item_tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_deployment_item_tasks_status_id",
                table: "deployment_item_tasks",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_item_tasks_deployment_statuses_status_id",
                table: "deployment_item_tasks",
                column: "status_id",
                principalTable: "deployment_statuses",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployment_item_tasks_deployment_statuses_status_id",
                table: "deployment_item_tasks");

            migrationBuilder.DropIndex(
                name: "IX_deployment_item_tasks_status_id",
                table: "deployment_item_tasks");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "deployment_item_tasks");
        }
    }
}
