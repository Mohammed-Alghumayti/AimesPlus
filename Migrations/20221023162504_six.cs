using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorProject.Migrations
{
    public partial class six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticulationMatrixAssessmentTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticulationMatrix_RefId = table.Column<int>(type: "int", nullable: false),
                    AssessmentTools_ReftoolId = table.Column<int>(type: "int", nullable: false),
                    WeekNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticulationMatrixAssessmentTools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticulationMatrixAssessmentTools_ArticulationMatrix_ArticulationMatrix_RefId",
                        column: x => x.ArticulationMatrix_RefId,
                        principalTable: "ArticulationMatrix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticulationMatrixAssessmentTools_AssessmentTools_AssessmentTools_ReftoolId",
                        column: x => x.AssessmentTools_ReftoolId,
                        principalTable: "AssessmentTools",
                        principalColumn: "toolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticulationMatrixAssessmentTools_ArticulationMatrix_RefId",
                table: "ArticulationMatrixAssessmentTools",
                column: "ArticulationMatrix_RefId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulationMatrixAssessmentTools_AssessmentTools_ReftoolId",
                table: "ArticulationMatrixAssessmentTools",
                column: "AssessmentTools_ReftoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticulationMatrixAssessmentTools");
        }
    }
}
