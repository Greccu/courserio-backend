using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courserio.Infrastructure.Migrations
{
    public partial class CourseChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiniatureImage",
                table: "Courses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiniatureImage",
                table: "Courses");
        }
    }
}
