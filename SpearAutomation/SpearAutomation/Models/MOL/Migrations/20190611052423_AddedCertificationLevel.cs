using Microsoft.EntityFrameworkCore.Migrations;

namespace SpearAutomation.Models.MOL.Migrations
{
    public partial class AddedCertificationLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CertificationLevel",
                table: "Personnel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificationLevel",
                table: "Personnel");
        }
    }
}
