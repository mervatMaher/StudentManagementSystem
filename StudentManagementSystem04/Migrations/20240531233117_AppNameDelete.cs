using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem04.Migrations
{
    /// <inheritdoc />
    public partial class AppNameDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppName",
                table: "UniProjects");

            migrationBuilder.DropColumn(
                name: "AppName",
                table: "PersonalProjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppName",
                table: "UniProjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppName",
                table: "PersonalProjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
