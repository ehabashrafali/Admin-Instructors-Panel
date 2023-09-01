using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_Panel_ITI.Migrations
{
    /// <inheritdoc />
    public partial class allowNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Instructor_InstructorID",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Instructor_InstructorID",
                table: "Material");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorID",
                table: "Material",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorID",
                table: "Exam",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Instructor_InstructorID",
                table: "Exam",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Instructor_InstructorID",
                table: "Material",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Instructor_InstructorID",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Instructor_InstructorID",
                table: "Material");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorID",
                table: "Material",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstructorID",
                table: "Exam",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Instructor_InstructorID",
                table: "Exam",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Instructor_InstructorID",
                table: "Material",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
