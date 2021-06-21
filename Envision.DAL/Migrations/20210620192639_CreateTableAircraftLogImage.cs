using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Envision.DAL.Migrations
{
    public partial class CreateTableAircraftLogImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AircraftLogImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AircraftLogId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageExtension = table.Column<string>(type: "varchar(10)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UploadBy = table.Column<int>(type: "int", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftLogImages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AircraftLogImages");
        }
    }
}
