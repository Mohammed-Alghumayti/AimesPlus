using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorProject.Migrations
{
    public partial class fixingGD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeDistribution_ArticulationMatrix_ArticulationMatrix_RefId",
                table: "GradeDistribution");

            migrationBuilder.DropIndex(
                name: "IX_GradeDistribution_ArticulationMatrix_RefId",
                table: "GradeDistribution");

            migrationBuilder.DropColumn(
                name: "ArticulationMatrix_RefId",
                table: "GradeDistribution");

            migrationBuilder.AlterColumn<int>(
                name: "week_Number",
                table: "GradeDistribution",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "percentage",
                table: "GradeDistribution",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "assessing_SO",
                table: "GradeDistribution",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "SO6",
                table: "GradeDistribution",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SO5",
                table: "GradeDistribution",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SO4",
                table: "GradeDistribution",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SO3",
                table: "GradeDistribution",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SO2",
                table: "GradeDistribution",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SO1",
                table: "GradeDistribution",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "week_Number",
                table: "GradeDistribution",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "percentage",
                table: "GradeDistribution",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "assessing_SO",
                table: "GradeDistribution",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SO6",
                table: "GradeDistribution",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SO5",
                table: "GradeDistribution",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SO4",
                table: "GradeDistribution",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SO3",
                table: "GradeDistribution",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SO2",
                table: "GradeDistribution",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SO1",
                table: "GradeDistribution",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArticulationMatrix_RefId",
                table: "GradeDistribution",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GradeDistribution_ArticulationMatrix_RefId",
                table: "GradeDistribution",
                column: "ArticulationMatrix_RefId");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeDistribution_ArticulationMatrix_ArticulationMatrix_RefId",
                table: "GradeDistribution",
                column: "ArticulationMatrix_RefId",
                principalTable: "ArticulationMatrix",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
