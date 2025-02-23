using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "project_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    modified_on = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    modified_by = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "squads",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    modified_on = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    modified_by = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_squads", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    modified_on = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    modified_by = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_projects_project_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "project_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_projects_category_id",
                table: "projects",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "squads");

            migrationBuilder.DropTable(
                name: "project_categories");
        }
    }
}
