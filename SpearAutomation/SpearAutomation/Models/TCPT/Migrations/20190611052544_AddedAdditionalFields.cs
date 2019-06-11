using Microsoft.EntityFrameworkCore.Migrations;

namespace SpearAutomation.Models.TCPT.Migrations
{
    public partial class AddedAdditionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CertificationLevel",
                table: "Resource",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleType",
                table: "Resource",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificationLevel",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "VehicleType",
                table: "Resource");
        }
    }
}
