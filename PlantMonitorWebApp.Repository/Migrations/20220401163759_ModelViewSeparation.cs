using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantMonitorWebApp.Repository.Migrations
{
    public partial class ModelViewSeparation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensorValue",
                table: "Sensor");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Sensor",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceUri",
                table: "Sensor",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceUri",
                table: "Sensor");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Sensor",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<double>(
                name: "SensorValue",
                table: "Sensor",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
