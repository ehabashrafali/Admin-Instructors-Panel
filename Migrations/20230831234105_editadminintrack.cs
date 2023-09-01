using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_Panel_ITI.Migrations
{
    /// <inheritdoc />
    public partial class editadminintrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intake_Admin_AdminID",
                table: "Intake");

            migrationBuilder.DropForeignKey(
                name: "FK_Track_Admin_AdminID",
                table: "Track");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Track",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Intake",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Intake",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Intake_Admin_AdminID",
                table: "Intake",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Track_Admin_AdminID",
                table: "Track",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intake_Admin_AdminID",
                table: "Intake");

            migrationBuilder.DropForeignKey(
                name: "FK_Track_Admin_AdminID",
                table: "Track");

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Track",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Intake",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Intake",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Intake_Admin_AdminID",
                table: "Intake",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Track_Admin_AdminID",
                table: "Track",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
