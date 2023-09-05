using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_Panel_ITI.Migrations
{
    /// <inheritdoc />
    public partial class delete_FKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Admin_AdminAspNetUserID",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Admin_AdminAspNetUserID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_AdminAspNetUserID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Instructor_AdminAspNetUserID",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "AdminAspNetUserID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "AdminAspNetUserID",
                table: "Instructor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminAspNetUserID",
                table: "Student",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminAspNetUserID",
                table: "Instructor",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_AdminAspNetUserID",
                table: "Student",
                column: "AdminAspNetUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_AdminAspNetUserID",
                table: "Instructor",
                column: "AdminAspNetUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Admin_AdminAspNetUserID",
                table: "Instructor",
                column: "AdminAspNetUserID",
                principalTable: "Admin",
                principalColumn: "AspNetUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Admin_AdminAspNetUserID",
                table: "Student",
                column: "AdminAspNetUserID",
                principalTable: "Admin",
                principalColumn: "AspNetUserID");
        }
    }
}
