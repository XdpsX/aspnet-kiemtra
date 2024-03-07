using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassManagement2.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollAtToEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollAt",
                table: "Enrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollAt",
                table: "Enrollments");
        }
    }
}
