using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueContraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_squads_code",
                table: "squads",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_squads_name",
                table: "squads",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_projects_code",
                table: "projects",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_projects_name",
                table: "projects",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_categories_code",
                table: "project_categories",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_categories_name",
                table: "project_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_environments_code",
                table: "environments",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_environments_name",
                table: "environments",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_deployments_name",
                table: "deployments",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_deployment_task_types_code",
                table: "deployment_task_types",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_deployment_task_types_name",
                table: "deployment_task_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_associates_email_id",
                table: "associates",
                column: "email_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_squads_code",
                table: "squads");

            migrationBuilder.DropIndex(
                name: "IX_squads_name",
                table: "squads");

            migrationBuilder.DropIndex(
                name: "IX_projects_code",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "IX_projects_name",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "IX_project_categories_code",
                table: "project_categories");

            migrationBuilder.DropIndex(
                name: "IX_project_categories_name",
                table: "project_categories");

            migrationBuilder.DropIndex(
                name: "IX_environments_code",
                table: "environments");

            migrationBuilder.DropIndex(
                name: "IX_environments_name",
                table: "environments");

            migrationBuilder.DropIndex(
                name: "IX_deployments_name",
                table: "deployments");

            migrationBuilder.DropIndex(
                name: "IX_deployment_task_types_code",
                table: "deployment_task_types");

            migrationBuilder.DropIndex(
                name: "IX_deployment_task_types_name",
                table: "deployment_task_types");

            migrationBuilder.DropIndex(
                name: "IX_associates_email_id",
                table: "associates");
        }
    }
}
