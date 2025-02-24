using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Deployments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deployments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    squad_id = table.Column<int>(type: "int", nullable: false),
                    environment_id = table.Column<int>(type: "int", nullable: false),
                    planned_start = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    planned_end = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
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
                    table.PrimaryKey("PK_deployments", x => x.id);
                    table.ForeignKey(
                        name: "FK_deployments_environments_environment_id",
                        column: x => x.environment_id,
                        principalTable: "environments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_deployments_squads_squad_id",
                        column: x => x.squad_id,
                        principalTable: "squads",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deployments_environment_id",
                table: "deployments",
                column: "environment_id");

            migrationBuilder.CreateIndex(
                name: "IX_deployments_squad_id",
                table: "deployments",
                column: "squad_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deployments");
        }
    }
}
