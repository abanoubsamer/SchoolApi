using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolWep.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class EditManger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Instructor_MangerId",
                table: "Instructor");

            migrationBuilder.AlterColumn<int>(
                name: "MangerId",
                table: "Instructor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Instructor_MangerId",
                table: "Instructor",
                column: "MangerId",
                principalTable: "Instructor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Instructor_MangerId",
                table: "Instructor");

            migrationBuilder.AlterColumn<int>(
                name: "MangerId",
                table: "Instructor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Instructor_MangerId",
                table: "Instructor",
                column: "MangerId",
                principalTable: "Instructor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
