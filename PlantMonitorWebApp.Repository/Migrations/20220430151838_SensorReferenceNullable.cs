using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantMonitorWebApp.Repository.Migrations
{
    public partial class SensorReferenceNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plant_Sensor_SensorId",
                table: "Plant");

            migrationBuilder.AlterColumn<int>(
                name: "SensorId",
                table: "Plant",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Plant_Sensor_SensorId",
                table: "Plant",
                column: "SensorId",
                principalTable: "Sensor",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plant_Sensor_SensorId",
                table: "Plant");

            migrationBuilder.AlterColumn<int>(
                name: "SensorId",
                table: "Plant",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Plant_Sensor_SensorId",
                table: "Plant",
                column: "SensorId",
                principalTable: "Sensor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
