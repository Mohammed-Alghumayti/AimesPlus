using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorProject.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssesmentTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssesmentTools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CLO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLO", x => x.Id);
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
                name: "inClassActs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inClassActs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LOD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutClassActs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutClassActs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marks = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicsId = table.Column<int>(type: "int", nullable: false),
                    CLOId = table.Column<int>(type: "int", nullable: false),
                    LODId = table.Column<int>(type: "int", nullable: false),
                    SOId = table.Column<int>(type: "int", nullable: false),
                    AssesSO = table.Column<bool>(type: "bit", nullable: false),
                    InClassId = table.Column<int>(type: "int", nullable: false),
                    OutClassId = table.Column<int>(type: "int", nullable: false),
                    AssesToolsId = table.Column<int>(type: "int", nullable: false),
                    SectionsId = table.Column<int>(type: "int", nullable: false),
                    Credit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_AssesmentTools_AssesToolsId",
                        column: x => x.AssesToolsId,
                        principalTable: "AssesmentTools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CLO_CLOId",
                        column: x => x.CLOId,
                        principalTable: "CLO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_inClassActs_InClassId",
                        column: x => x.InClassId,
                        principalTable: "inClassActs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_LOD_LODId",
                        column: x => x.LODId,
                        principalTable: "LOD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_OutClassActs_OutClassId",
                        column: x => x.OutClassId,
                        principalTable: "OutClassActs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_SO_SOId",
                        column: x => x.SOId,
                        principalTable: "SO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoursesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentsId = table.Column<int>(type: "int", nullable: false),
                    instructorsId = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    semester = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sections_Instructors_instructorsId",
                        column: x => x.instructorsId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sections_Students_studentsId",
                        column: x => x.studentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AssesToolsId",
                table: "Courses",
                column: "AssesToolsId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CLOId",
                table: "Courses",
                column: "CLOId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InClassId",
                table: "Courses",
                column: "InClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LODId",
                table: "Courses",
                column: "LODId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OutClassId",
                table: "Courses",
                column: "OutClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SectionsId",
                table: "Courses",
                column: "SectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SOId",
                table: "Courses",
                column: "SOId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TopicsId",
                table: "Courses",
                column: "TopicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_CoursesId",
                table: "Instructors",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_sections_instructorsId",
                table: "sections",
                column: "instructorsId");

            migrationBuilder.CreateIndex(
                name: "IX_sections_studentsId",
                table: "sections",
                column: "studentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_sections_SectionsId",
                table: "Courses",
                column: "SectionsId",
                principalTable: "sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AssesmentTools_AssesToolsId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CLO_CLOId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_inClassActs_InClassId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_LOD_LODId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_OutClassActs_OutClassId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_sections_SectionsId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "FacultyMembers");

            migrationBuilder.DropTable(
                name: "AssesmentTools");

            migrationBuilder.DropTable(
                name: "CLO");

            migrationBuilder.DropTable(
                name: "inClassActs");

            migrationBuilder.DropTable(
                name: "LOD");

            migrationBuilder.DropTable(
                name: "OutClassActs");

            migrationBuilder.DropTable(
                name: "sections");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "SO");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
