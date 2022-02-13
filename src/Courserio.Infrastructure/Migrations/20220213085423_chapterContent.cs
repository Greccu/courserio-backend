using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courserio.Infrastructure.Migrations
{
    public partial class chapterContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Chapters");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Chapters",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Chapters");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Chapters",
                type: "int",
                nullable: true);
        }
    }
}
