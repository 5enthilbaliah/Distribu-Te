using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class DeploymentStatusesForeignKeys3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployments_deployment_statuses_status_id",
                table: "deployments");

            migrationBuilder.AddForeignKey(
                name: "FK_deployments_deployment_statuses_status_id",
                table: "deployments",
                column: "status_id",
                principalTable: "deployment_statuses",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployments_deployment_statuses_status_id",
                table: "deployments");

            migrationBuilder.AddForeignKey(
                name: "FK_deployments_deployment_statuses_status_id",
                table: "deployments",
                column: "status_id",
                principalTable: "deployment_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
