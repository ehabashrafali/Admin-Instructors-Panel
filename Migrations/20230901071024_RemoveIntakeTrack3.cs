using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_Panel_ITI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIntakeTrack3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intake_Track");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Intake_Track",
                columns: table => new
                {
                    TrackID = table.Column<int>(type: "int", nullable: false),
                    IntakeID = table.Column<int>(type: "int", nullable: false),
                    NumOfStdsInTrack = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intake_Track", x => new { x.TrackID, x.IntakeID });
                    table.ForeignKey(
                        name: "FK_Intake_Track_Intake_IntakeID",
                        column: x => x.IntakeID,
                        principalTable: "Intake",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Intake_Track_Track_TrackID",
                        column: x => x.TrackID,
                        principalTable: "Track",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intake_Track_IntakeID",
                table: "Intake_Track",
                column: "IntakeID");
        }
    }
}
