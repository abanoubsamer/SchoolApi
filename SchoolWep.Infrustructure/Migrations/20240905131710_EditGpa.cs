using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolWep.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class EditGpa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesDepartment_Courses_CourseId",
                table: "CoursesDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesDepartment_Departments_DepartmentId",
                table: "CoursesDepartment");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gpa",
                table: "Students",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesDepartment_Courses_CourseId",
                table: "CoursesDepartment",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesDepartment_Departments_DepartmentId",
                table: "CoursesDepartment",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesDepartment_Courses_CourseId",
                table: "CoursesDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesDepartment_Departments_DepartmentId",
                table: "CoursesDepartment");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gpa",
                table: "Students",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldDefaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesDepartment_Courses_CourseId",
                table: "CoursesDepartment",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesDepartment_Departments_DepartmentId",
                table: "CoursesDepartment",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
