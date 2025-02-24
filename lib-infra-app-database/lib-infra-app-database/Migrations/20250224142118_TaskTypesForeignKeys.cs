using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class TaskTypesForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "task_type_id",
                table: "deployment_item_tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_deployment_item_tasks_task_type_id",
                table: "deployment_item_tasks",
                column: "task_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_item_tasks_deployment_task_types_task_type_id",
                table: "deployment_item_tasks",
                column: "task_type_id",
                principalTable: "deployment_task_types",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployment_item_tasks_deployment_task_types_task_type_id",
                table: "deployment_item_tasks");

            migrationBuilder.DropIndex(
                name: "IX_deployment_item_tasks_task_type_id",
                table: "deployment_item_tasks");

            migrationBuilder.DropColumn(
                name: "task_type_id",
                table: "deployment_item_tasks");
        }
    }
}
