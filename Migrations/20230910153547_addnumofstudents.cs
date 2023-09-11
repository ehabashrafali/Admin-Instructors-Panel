using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_Panel_ITI.Migrations
{
    /// <inheritdoc />
    public partial class addnumofstudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "numOfStudentsInCourse",
                table: "Intake_Track_Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numOfStudentsInCourse",
                table: "Intake_Track_Courses");
        }
    }
}
