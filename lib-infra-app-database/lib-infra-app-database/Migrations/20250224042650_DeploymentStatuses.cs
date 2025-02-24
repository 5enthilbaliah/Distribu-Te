using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class DeploymentStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "deployments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "deployment_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    modified_on = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deployment_statuses", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deployments_status_id",
                table: "deployments",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_deployments_deployment_statuses_status_id",
                table: "deployments",
                column: "status_id",
                principalTable: "deployment_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deployments_deployment_statuses_status_id",
                table: "deployments");

            migrationBuilder.DropTable(
                name: "deployment_statuses");

            migrationBuilder.DropIndex(
                name: "IX_deployments_status_id",
                table: "deployments");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "deployments");
        }
    }
}
