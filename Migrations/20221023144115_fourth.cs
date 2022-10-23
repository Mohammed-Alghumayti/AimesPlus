using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorProject.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticulationMatrix_TeachersCourse_teacherCourse_RefteacherCourse_Id",
                table: "ArticulationMatrix");

            migrationBuilder.RenameColumn(
                name: "teacherCourse_RefteacherCourse_Id",
                table: "ArticulationMatrix",
                newName: "course_Refcourse_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ArticulationMatrix_teacherCourse_RefteacherCourse_Id",
                table: "ArticulationMatrix",
                newName: "IX_ArticulationMatrix_course_Refcourse_Id");

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

            migrationBuilder.RenameColumn(
                name: "course_Refcourse_Id",
                table: "ArticulationMatrix",
                newName: "teacherCourse_RefteacherCourse_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ArticulationMatrix_course_Refcourse_Id",
                table: "ArticulationMatrix",
                newName: "IX_ArticulationMatrix_teacherCourse_RefteacherCourse_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticulationMatrix_TeachersCourse_teacherCourse_RefteacherCourse_Id",
                table: "ArticulationMatrix",
                column: "teacherCourse_RefteacherCourse_Id",
                principalTable: "TeachersCourse",
                principalColumn: "teacherCourse_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
