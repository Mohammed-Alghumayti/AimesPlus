using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorProject.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticulationMatrix_GradeDistribution_GradeDistributionId",
                table: "ArticulationMatrix");

            migrationBuilder.DropIndex(
                name: "IX_ArticulationMatrix_GradeDistributionId",
                table: "ArticulationMatrix");

            migrationBuilder.DropColumn(
                name: "GradeDistributionId",
                table: "ArticulationMatrix");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "GradeDistribution",
                newName: "coursecode");

            migrationBuilder.AddColumn<string>(
                name: "Assessment",
                table: "GradeDistribution",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SOchoice",
                table: "GradeDistribution",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assessment",
                table: "GradeDistribution");

            migrationBuilder.DropColumn(
                name: "SOchoice",
                table: "GradeDistribution");

            migrationBuilder.RenameColumn(
                name: "coursecode",
                table: "GradeDistribution",
                newName: "title");

            migrationBuilder.AddColumn<int>(
                name: "GradeDistributionId",
                table: "ArticulationMatrix",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticulationMatrix_GradeDistributionId",
                table: "ArticulationMatrix",
                column: "GradeDistributionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticulationMatrix_GradeDistribution_GradeDistributionId",
                table: "ArticulationMatrix",
                column: "GradeDistributionId",
                principalTable: "GradeDistribution",
                principalColumn: "Id");
        }
    }
}
