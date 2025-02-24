using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AssociateForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployment_item_tasks_associates_associate_id",
                table: "deployment_item_tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_item_tasks_associates_associate_id",
                table: "deployment_item_tasks",
                column: "associate_id",
                principalTable: "associates",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployment_item_tasks_associates_associate_id",
                table: "deployment_item_tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_item_tasks_associates_associate_id",
                table: "deployment_item_tasks",
                column: "associate_id",
                principalTable: "associates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
