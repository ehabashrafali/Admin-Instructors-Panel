using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_Panel_ITI.Migrations
{
    /// <inheritdoc />
    public partial class courseadminid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Admin_AdminID",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Course",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Admin_AdminID",
                table: "Course",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "AspNetUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Admin_AdminID",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Course",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Admin_AdminID",
                table: "Course",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "AspNetUserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
