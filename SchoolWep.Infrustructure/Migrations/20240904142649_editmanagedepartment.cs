using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolWep.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class editmanagedepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructor_InstManger",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InstManger",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "InstManger",
                table: "Departments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InstManger",
                table: "Departments",
                column: "InstManger",
                unique: true,
                filter: "[InstManger] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructor_InstManger",
                table: "Departments",
                column: "InstManger",
                principalTable: "Instructor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructor_InstManger",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InstManger",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "InstManger",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InstManger",
                table: "Departments",
                column: "InstManger",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructor_InstManger",
                table: "Departments",
                column: "InstManger",
                principalTable: "Instructor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
