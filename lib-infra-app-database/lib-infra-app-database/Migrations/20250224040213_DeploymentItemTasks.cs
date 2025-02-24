using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class DeploymentItemTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deployment_item_tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deployment_item_id = table.Column<int>(type: "int", nullable: false),
                    associate_id = table.Column<int>(type: "int", nullable: false),
                    sequence = table.Column<int>(type: "int", nullable: false),
                    actual_start = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    actual_end = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    comments = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    modified_on = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deployment_item_tasks", x => x.id);
                    table.ForeignKey(
                        name: "FK_deployment_item_tasks_associates_associate_id",
                        column: x => x.associate_id,
                        principalTable: "associates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_deployment_item_tasks_deployment_items_deployment_item_id",
                        column: x => x.deployment_item_id,
                        principalTable: "deployment_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deployment_item_tasks_associate_id",
                table: "deployment_item_tasks",
                column: "associate_id");

            migrationBuilder.CreateIndex(
                name: "IX_deployment_item_tasks_deployment_item_id",
                table: "deployment_item_tasks",
                column: "deployment_item_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deployment_item_tasks");
        }
    }
}
