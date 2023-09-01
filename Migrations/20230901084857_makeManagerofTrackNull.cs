using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_Panel_ITI.Migrations
{
    /// <inheritdoc />
    public partial class makeManagerofTrackNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Track_Instructor_ManagerID",
                table: "Track");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerID",
                table: "Track",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Track_Instructor_ManagerID",
                table: "Track",
                column: "ManagerID",
                principalTable: "Instructor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Track_Instructor_ManagerID",
                table: "Track");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerID",
                table: "Track",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Track_Instructor_ManagerID",
                table: "Track",
                column: "ManagerID",
                principalTable: "Instructor",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
