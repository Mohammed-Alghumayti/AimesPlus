using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorProject.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeekNo",
                table: "CourseCatalog",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekNo",
                table: "CourseCatalog");
        }
    }
}
