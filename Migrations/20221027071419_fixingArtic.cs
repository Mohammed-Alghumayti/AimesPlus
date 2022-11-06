using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorProject.Migrations
{
    public partial class fixingArtic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticulationMatrix_Courses_courseId",
                table: "ArticulationMatrix");

            migrationBuilder.DropIndex(
                name: "IX_ArticulationMatrix_courseId",
                table: "ArticulationMatrix");

            migrationBuilder.RenameColumn(
                name: "courseId",
                table: "ArticulationMatrix",
                newName: "course_Refcourse_Id");

            migrationBuilder.AddColumn<string>(
                name: "course_Code",
                table: "ArticulationMatrix",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulationMatrix_course_Refcourse_Id",
                table: "ArticulationMatrix",
                column: "course_Refcourse_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticulationMatrix_Courses_course_Refcourse_Id",
                table: "ArticulationMatrix",
                column: "course_Refcourse_Id",
                principalTable: "Courses",
                principalColumn: "course_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticulationMatrix_Courses_course_Refcourse_Id",
                table: "ArticulationMatrix");

            migrationBuilder.DropIndex(
                name: "IX_ArticulationMatrix_course_Refcourse_Id",
                table: "ArticulationMatrix");

            migrationBuilder.DropColumn(
                name: "course_Code",
                table: "ArticulationMatrix");

            migrationBuilder.RenameColumn(
                name: "course_Refcourse_Id",
                table: "ArticulationMatrix",
                newName: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulationMatrix_courseId",
                table: "ArticulationMatrix",
                column: "courseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticulationMatrix_Courses_courseId",
                table: "ArticulationMatrix",
                column: "courseId",
                principalTable: "Courses",
                principalColumn: "course_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
