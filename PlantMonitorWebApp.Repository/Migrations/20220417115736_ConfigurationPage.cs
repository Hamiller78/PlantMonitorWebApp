using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantMonitorWebApp.Repository.Migrations
{
    public partial class ConfigurationPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sensor",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sensor");
        }
    }
}
