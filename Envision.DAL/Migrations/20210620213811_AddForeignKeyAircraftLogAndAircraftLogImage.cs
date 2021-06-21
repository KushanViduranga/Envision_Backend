using Microsoft.EntityFrameworkCore.Migrations;

namespace Envision.DAL.Migrations
{
    public partial class AddForeignKeyAircraftLogAndAircraftLogImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AircraftLogImages_AircraftLogId",
                table: "AircraftLogImages",
                column: "AircraftLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftLogImages_AircraftLogs_AircraftLogId",
                table: "AircraftLogImages",
                column: "AircraftLogId",
                principalTable: "AircraftLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftLogImages_AircraftLogs_AircraftLogId",
                table: "AircraftLogImages");

            migrationBuilder.DropIndex(
                name: "IX_AircraftLogImages_AircraftLogId",
                table: "AircraftLogImages");
        }
    }
}
