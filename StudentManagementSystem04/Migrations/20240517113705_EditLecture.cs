using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem04.Migrations
{
    /// <inheritdoc />
    public partial class EditLecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Lectures");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Lectures",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Lectures",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Lectures");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Lectures",
                newName: "DateTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Lectures",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Lectures",
                type: "time",
                nullable: true);
        }
    }
}
