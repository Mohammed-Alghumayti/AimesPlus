using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorProject.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Activities_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    activity_Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Activities_Id);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentTools",
                columns: table => new
                {
                    toolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentTools", x => x.toolId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    course_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    course_Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.course_Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Dept_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Dept_Id);
                });

            migrationBuilder.CreateTable(
                name: "FacultyMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LOD",
                columns: table => new
                {
                    LOD_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOD_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LOD_Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOD", x => x.LOD_Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    teacher_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teacher_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.teacher_Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_Refcourse_Id = table.Column<int>(type: "int", nullable: false),
                    catalog_topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCatalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCatalog_Courses_course_Refcourse_Id",
                        column: x => x.course_Refcourse_Id,
                        principalTable: "Courses",
                        principalColumn: "course_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    session_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dept_RefDept_Id = table.Column<int>(type: "int", nullable: false),
                    startDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    endDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.session_Id);
                    table.ForeignKey(
                        name: "FK_Session_Department_Dept_RefDept_Id",
                        column: x => x.Dept_RefDept_Id,
                        principalTable: "Department",
                        principalColumn: "Dept_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachersCourse",
                columns: table => new
                {
                    teacherCourse_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_Refcourse_Id = table.Column<int>(type: "int", nullable: false),
                    teacher_Refteacher_Id = table.Column<int>(type: "int", nullable: false),
                    SemesterStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemesterEnd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersCourse", x => x.teacherCourse_Id);
                    table.ForeignKey(
                        name: "FK_TeachersCourse_Courses_course_Refcourse_Id",
                        column: x => x.course_Refcourse_Id,
                        principalTable: "Courses",
                        principalColumn: "course_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersCourse_Teachers_teacher_Refteacher_Id",
                        column: x => x.teacher_Refteacher_Id,
                        principalTable: "Teachers",
                        principalColumn: "teacher_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    StuCourse_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_RefStudentId = table.Column<int>(type: "int", nullable: false),
                    course_Refcourse_Id = table.Column<int>(type: "int", nullable: false),
                    session_Refsession_Id = table.Column<int>(type: "int", nullable: false),
                    marks = table.Column<double>(type: "float", nullable: false),
                    result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => x.StuCourse_Id);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Courses_course_Refcourse_Id",
                        column: x => x.course_Refcourse_Id,
                        principalTable: "Courses",
                        principalColumn: "course_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Session_session_Refsession_Id",
                        column: x => x.session_Refsession_Id,
                        principalTable: "Session",
                        principalColumn: "session_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Student_Student_RefStudentId",
                        column: x => x.Student_RefStudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticulationMatrix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teacherCourse_RefteacherCourse_Id = table.Column<int>(type: "int", nullable: false),
                    CLO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LOD_RefLOD_Id = table.Column<int>(type: "int", nullable: false),
                    SO = table.Column<int>(type: "int", nullable: false),
                    Assessing_SO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticulationMatrix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticulationMatrix_LOD_LOD_RefLOD_Id",
                        column: x => x.LOD_RefLOD_Id,
                        principalTable: "LOD",
                        principalColumn: "LOD_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticulationMatrix_TeachersCourse_teacherCourse_RefteacherCourse_Id",
                        column: x => x.teacherCourse_RefteacherCourse_Id,
                        principalTable: "TeachersCourse",
                        principalColumn: "teacherCourse_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachingSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teacherCourse_RefteacherCourse_Id = table.Column<int>(type: "int", nullable: false),
                    week_Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachingSchedule_TeachersCourse_teacherCourse_RefteacherCourse_Id",
                        column: x => x.teacherCourse_RefteacherCourse_Id,
                        principalTable: "TeachersCourse",
                        principalColumn: "teacherCourse_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourseActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuCourse_RefStuCourse_Id = table.Column<int>(type: "int", nullable: false),
                    Activities_RefActivities_Id = table.Column<int>(type: "int", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourseActivities_Activities_Activities_RefActivities_Id",
                        column: x => x.Activities_RefActivities_Id,
                        principalTable: "Activities",
                        principalColumn: "Activities_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourseActivities_StudentCourse_StuCourse_RefStuCourse_Id",
                        column: x => x.StuCourse_RefStuCourse_Id,
                        principalTable: "StudentCourse",
                        principalColumn: "StuCourse_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourseAssessmentTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuCourse_RefStuCourse_Id = table.Column<int>(type: "int", nullable: false),
                    tool_ReftoolId = table.Column<int>(type: "int", nullable: false),
                    marks = table.Column<double>(type: "float", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseAssessmentTools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourseAssessmentTools_AssessmentTools_tool_ReftoolId",
                        column: x => x.tool_ReftoolId,
                        principalTable: "AssessmentTools",
                        principalColumn: "toolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourseAssessmentTools_StudentCourse_StuCourse_RefStuCourse_Id",
                        column: x => x.StuCourse_RefStuCourse_Id,
                        principalTable: "StudentCourse",
                        principalColumn: "StuCourse_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticulationMatrixActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticulationMatrix_RefId = table.Column<int>(type: "int", nullable: false),
                    activity_RefActivities_Id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticulationMatrixActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticulationMatrixActivities_Activities_activity_RefActivities_Id",
                        column: x => x.activity_RefActivities_Id,
                        principalTable: "Activities",
                        principalColumn: "Activities_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticulationMatrixActivities_ArticulationMatrix_ArticulationMatrix_RefId",
                        column: x => x.ArticulationMatrix_RefId,
                        principalTable: "ArticulationMatrix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeDistribution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticulationMatrix_RefId = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    week_Number = table.Column<int>(type: "int", nullable: false),
                    SO1 = table.Column<int>(type: "int", nullable: false),
                    SO2 = table.Column<int>(type: "int", nullable: false),
                    SO3 = table.Column<int>(type: "int", nullable: false),
                    SO4 = table.Column<int>(type: "int", nullable: false),
                    SO5 = table.Column<int>(type: "int", nullable: false),
                    SO6 = table.Column<int>(type: "int", nullable: false),
                    percentage = table.Column<double>(type: "float", nullable: false),
                    assessing_SO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeDistribution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeDistribution_ArticulationMatrix_ArticulationMatrix_RefId",
                        column: x => x.ArticulationMatrix_RefId,
                        principalTable: "ArticulationMatrix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticulationMatrix_LOD_RefLOD_Id",
                table: "ArticulationMatrix",
                column: "LOD_RefLOD_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulationMatrix_teacherCourse_RefteacherCourse_Id",
                table: "ArticulationMatrix",
                column: "teacherCourse_RefteacherCourse_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulationMatrixActivities_activity_RefActivities_Id",
                table: "ArticulationMatrixActivities",
                column: "activity_RefActivities_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulationMatrixActivities_ArticulationMatrix_RefId",
                table: "ArticulationMatrixActivities",
                column: "ArticulationMatrix_RefId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCatalog_course_Refcourse_Id",
                table: "CourseCatalog",
                column: "course_Refcourse_Id");

            migrationBuilder.CreateIndex(
                name: "IX_GradeDistribution_ArticulationMatrix_RefId",
                table: "GradeDistribution",
                column: "ArticulationMatrix_RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_Dept_RefDept_Id",
                table: "Session",
                column: "Dept_RefDept_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_course_Refcourse_Id",
                table: "StudentCourse",
                column: "course_Refcourse_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_session_Refsession_Id",
                table: "StudentCourse",
                column: "session_Refsession_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_Student_RefStudentId",
                table: "StudentCourse",
                column: "Student_RefStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseActivities_Activities_RefActivities_Id",
                table: "StudentCourseActivities",
                column: "Activities_RefActivities_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseActivities_StuCourse_RefStuCourse_Id",
                table: "StudentCourseActivities",
                column: "StuCourse_RefStuCourse_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseAssessmentTools_StuCourse_RefStuCourse_Id",
                table: "StudentCourseAssessmentTools",
                column: "StuCourse_RefStuCourse_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseAssessmentTools_tool_ReftoolId",
                table: "StudentCourseAssessmentTools",
                column: "tool_ReftoolId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersCourse_course_Refcourse_Id",
                table: "TeachersCourse",
                column: "course_Refcourse_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersCourse_teacher_Refteacher_Id",
                table: "TeachersCourse",
                column: "teacher_Refteacher_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSchedule_teacherCourse_RefteacherCourse_Id",
                table: "TeachingSchedule",
                column: "teacherCourse_RefteacherCourse_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticulationMatrixActivities");

            migrationBuilder.DropTable(
                name: "CourseCatalog");

            migrationBuilder.DropTable(
                name: "FacultyMembers");

            migrationBuilder.DropTable(
                name: "GradeDistribution");

            migrationBuilder.DropTable(
                name: "StudentCourseActivities");

            migrationBuilder.DropTable(
                name: "StudentCourseAssessmentTools");

            migrationBuilder.DropTable(
                name: "TeachingSchedule");

            migrationBuilder.DropTable(
                name: "ArticulationMatrix");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "AssessmentTools");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "LOD");

            migrationBuilder.DropTable(
                name: "TeachersCourse");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
