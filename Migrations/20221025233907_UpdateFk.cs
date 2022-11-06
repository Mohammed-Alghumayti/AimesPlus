using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorProject.Migrations
{
    public partial class UpdateFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticulationMatrix_Courses_course_Refcourse_Id",
                table: "ArticulationMatrix");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticulationMatrix_LOD_LOD_RefLOD_Id",
                table: "ArticulationMatrix");

            migrationBuilder.DropIndex(
                name: "IX_ArticulationMatrix_course_Refcourse_Id",
                table: "ArticulationMatrix");

            migrationBuilder.RenameColumn(
                name: "course_Refcourse_Id",
                table: "ArticulationMatrix",
                newName: "courseId");

            migrationBuilder.RenameColumn(
                name: "LOD_RefLOD_Id",
                table: "ArticulationMatrix",
                newName: "LOdID");

            migrationBuilder.RenameIndex(
                name: "IX_ArticulationMatrix_LOD_RefLOD_Id",
                table: "ArticulationMatrix",
                newName: "IX_ArticulationMatrix_LOdID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ArticulationMatrix_LOD_LOdID",
                table: "ArticulationMatrix",
                column: "LOdID",
                principalTable: "LOD",
                principalColumn: "LOD_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticulationMatrix_Courses_courseId",
                table: "ArticulationMatrix");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticulationMatrix_LOD_LOdID",
                table: "ArticulationMatrix");

            migrationBuilder.DropIndex(
                name: "IX_ArticulationMatrix_courseId",
                table: "ArticulationMatrix");

            migrationBuilder.RenameColumn(
                name: "courseId",
                table: "ArticulationMatrix",
                newName: "course_Refcourse_Id");

            migrationBuilder.RenameColumn(
                name: "LOdID",
                table: "ArticulationMatrix",
                newName: "LOD_RefLOD_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ArticulationMatrix_LOdID",
                table: "ArticulationMatrix",
                newName: "IX_ArticulationMatrix_LOD_RefLOD_Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ArticulationMatrix_LOD_LOD_RefLOD_Id",
                table: "ArticulationMatrix",
                column: "LOD_RefLOD_Id",
                principalTable: "LOD",
                principalColumn: "LOD_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
