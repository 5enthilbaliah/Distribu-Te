using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class ProjectForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployment_items_projects_project_id",
                table: "deployment_items");

            migrationBuilder.DropForeignKey(
                name: "FK_deployments_squads_squad_id",
                table: "deployments");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_project_categories_category_id",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "FK_squad_associates_associates_associate_id",
                table: "squad_associates");

            migrationBuilder.DropForeignKey(
                name: "FK_squad_associates_squads_squad_id",
                table: "squad_associates");

            migrationBuilder.DropForeignKey(
                name: "FK_squad_projects_projects_project_id",
                table: "squad_projects");

            migrationBuilder.DropForeignKey(
                name: "FK_squad_projects_squads_squad_id",
                table: "squad_projects");

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_items_projects_project_id",
                table: "deployment_items",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_deployments_squads_squad_id",
                table: "deployments",
                column: "squad_id",
                principalTable: "squads",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_project_categories_category_id",
                table: "projects",
                column: "category_id",
                principalTable: "project_categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_squad_associates_associates_associate_id",
                table: "squad_associates",
                column: "associate_id",
                principalTable: "associates",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_squad_associates_squads_squad_id",
                table: "squad_associates",
                column: "squad_id",
                principalTable: "squads",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_squad_projects_projects_project_id",
                table: "squad_projects",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_squad_projects_squads_squad_id",
                table: "squad_projects",
                column: "squad_id",
                principalTable: "squads",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployment_items_projects_project_id",
                table: "deployment_items");

            migrationBuilder.DropForeignKey(
                name: "FK_deployments_squads_squad_id",
                table: "deployments");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_project_categories_category_id",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "FK_squad_associates_associates_associate_id",
                table: "squad_associates");

            migrationBuilder.DropForeignKey(
                name: "FK_squad_associates_squads_squad_id",
                table: "squad_associates");

            migrationBuilder.DropForeignKey(
                name: "FK_squad_projects_projects_project_id",
                table: "squad_projects");

            migrationBuilder.DropForeignKey(
                name: "FK_squad_projects_squads_squad_id",
                table: "squad_projects");

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_items_projects_project_id",
                table: "deployment_items",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_deployments_squads_squad_id",
                table: "deployments",
                column: "squad_id",
                principalTable: "squads",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projects_project_categories_category_id",
                table: "projects",
                column: "category_id",
                principalTable: "project_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_squad_associates_associates_associate_id",
                table: "squad_associates",
                column: "associate_id",
                principalTable: "associates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_squad_associates_squads_squad_id",
                table: "squad_associates",
                column: "squad_id",
                principalTable: "squads",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_squad_projects_projects_project_id",
                table: "squad_projects",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_squad_projects_squads_squad_id",
                table: "squad_projects",
                column: "squad_id",
                principalTable: "squads",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
