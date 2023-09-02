using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_Panel_ITI.Migrations
{
    /// <inheritdoc />
    public partial class updateusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_AspNetUsers_Id",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Admin_AdminId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Admin_AdminID",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_AspNetUsers_Id",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Admin_AdminID",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_Id",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Student",
                newName: "AspNetUserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Instructor",
                newName: "AspNetUserID");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Exam",
                newName: "AdminAspNetUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Exam_AdminId",
                table: "Exam",
                newName: "IX_Exam_AdminAspNetUserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Admin",
                newName: "AspNetUserID");

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Student",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AdminAspNetUserID",
                table: "Student",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Instructor",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
                name: "FK_Admin_AspNetUsers_AspNetUserID",
                table: "Admin",
                column: "AspNetUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Admin_AdminAspNetUserID",
                table: "Exam",
                column: "AdminAspNetUserID",
                principalTable: "Admin",
                principalColumn: "AspNetUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Admin_AdminAspNetUserID",
                table: "Instructor",
                column: "AdminAspNetUserID",
                principalTable: "Admin",
                principalColumn: "AspNetUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_AspNetUsers_AdminID",
                table: "Instructor",
                column: "AdminID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_AspNetUsers_AspNetUserID",
                table: "Instructor",
                column: "AspNetUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Admin_AdminAspNetUserID",
                table: "Student",
                column: "AdminAspNetUserID",
                principalTable: "Admin",
                principalColumn: "AspNetUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_AdminID",
                table: "Student",
                column: "AdminID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_AspNetUserID",
                table: "Student",
                column: "AspNetUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_AspNetUsers_AspNetUserID",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Admin_AdminAspNetUserID",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Admin_AdminAspNetUserID",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_AspNetUsers_AdminID",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_AspNetUsers_AspNetUserID",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Admin_AdminAspNetUserID",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_AdminID",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_AspNetUserID",
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

            migrationBuilder.RenameColumn(
                name: "AspNetUserID",
                table: "Student",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AspNetUserID",
                table: "Instructor",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AdminAspNetUserID",
                table: "Exam",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Exam_AdminAspNetUserID",
                table: "Exam",
                newName: "IX_Exam_AdminId");

            migrationBuilder.RenameColumn(
                name: "AspNetUserID",
                table: "Admin",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Student",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Instructor",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_AspNetUsers_Id",
                table: "Admin",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Admin_AdminId",
                table: "Exam",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Admin_AdminID",
                table: "Instructor",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_AspNetUsers_Id",
                table: "Instructor",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Admin_AdminID",
                table: "Student",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_Id",
                table: "Student",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
