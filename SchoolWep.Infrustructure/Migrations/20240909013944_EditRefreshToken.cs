using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolWep.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class EditRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JwtId",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "JwtId",
                table: "RefreshToken");
        }
    }
}
