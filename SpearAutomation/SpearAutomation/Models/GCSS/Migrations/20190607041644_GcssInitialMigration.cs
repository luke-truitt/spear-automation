using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpearAutomation.Models.GCSS.Migrations
{
    public partial class GcssInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Tam = table.Column<int>(nullable: false),
                    DateAvailable = table.Column<DateTime>(nullable: false),
                    Location = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Tam);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
