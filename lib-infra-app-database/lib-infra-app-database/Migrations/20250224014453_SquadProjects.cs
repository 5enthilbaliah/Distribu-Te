using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class SquadProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "squad_projects",
                columns: table => new
                {
                    squad_id = table.Column<int>(type: "int", nullable: false),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    started_on = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    ended_on = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    modified_on = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_squad_projects", x => new { x.squad_id, x.project_id });
                    table.ForeignKey(
                        name: "FK_squad_projects_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_squad_projects_squads_squad_id",
                        column: x => x.squad_id,
                        principalTable: "squads",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_squad_projects_project_id",
                table: "squad_projects",
                column: "project_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "squad_projects");
        }
    }
}
