using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpearAutomation.Models.MOL.Migrations
{
    public partial class MolInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Personnel",
            //    columns: table => new
            //    {
            //        MarineId = table.Column<int>(nullable: false),
            //        DateReturning = table.Column<DateTime>(nullable: false),
            //        Location = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Personnel", x => x.MarineId);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personnel");
        }
    }
}
