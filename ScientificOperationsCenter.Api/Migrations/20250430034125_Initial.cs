using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificOperationsCenter.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RadiationMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    Milligrays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadiationMeasurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temperatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    TemperatureCelcius = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperatures", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RadiationMeasurements",
                columns: ["Id", "Date", "Milligrays", "Time"],
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 9, 8), 100, new TimeOnly(16, 0, 0) },
                    { 2, new DateOnly(2024, 9, 8), 140, new TimeOnly(15, 0, 0) },
                    { 3, new DateOnly(2024, 9, 8), 162, new TimeOnly(12, 0, 0) },
                    { 4, new DateOnly(2024, 10, 8), 100, new TimeOnly(16, 0, 0) },
                    { 5, new DateOnly(2024, 10, 8), 120, new TimeOnly(19, 0, 0) },
                    { 6, new DateOnly(2024, 10, 8), 190, new TimeOnly(12, 0, 0) },
                    { 7, new DateOnly(2024, 10, 9), 120, new TimeOnly(21, 30, 0) },
                    { 8, new DateOnly(2024, 10, 9), 110, new TimeOnly(21, 0, 0) },
                    { 9, new DateOnly(2024, 10, 9), 160, new TimeOnly(6, 0, 0) },
                    { 10, new DateOnly(2024, 11, 2), 100, new TimeOnly(9, 10, 0) },
                    { 11, new DateOnly(2024, 11, 3), 200, new TimeOnly(2, 30, 0) },
                    { 12, new DateOnly(2024, 12, 1), 120, new TimeOnly(4, 20, 0) },
                    { 13, new DateOnly(2024, 12, 2), 132, new TimeOnly(4, 10, 0) },
                    { 14, new DateOnly(2024, 12, 5), 126, new TimeOnly(7, 30, 0) },
                    { 15, new DateOnly(2025, 1, 3), 200, new TimeOnly(4, 30, 0) },
                    { 16, new DateOnly(2025, 1, 5), 200, new TimeOnly(4, 30, 0) }
                });

            migrationBuilder.InsertData(
                table: "Temperatures",
                columns: ["Id", "Date", "TemperatureCelcius", "Time"],
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 9, 2), 20, new TimeOnly(16, 0, 0) },
                    { 2, new DateOnly(2024, 9, 5), 15, new TimeOnly(15, 0, 0) },
                    { 3, new DateOnly(2024, 9, 7), -10, new TimeOnly(12, 0, 0) },
                    { 4, new DateOnly(2024, 10, 8), -4, new TimeOnly(16, 0, 0) },
                    { 5, new DateOnly(2024, 10, 8), 12, new TimeOnly(19, 0, 0) },
                    { 6, new DateOnly(2024, 10, 8), 11, new TimeOnly(12, 0, 0) },
                    { 7, new DateOnly(2024, 10, 9), 12, new TimeOnly(21, 30, 0) },
                    { 8, new DateOnly(2024, 10, 9), 10, new TimeOnly(21, 0, 0) },
                    { 9, new DateOnly(2024, 10, 9), 9, new TimeOnly(6, 0, 0) },
                    { 10, new DateOnly(2024, 11, 2), -2, new TimeOnly(9, 10, 0) },
                    { 11, new DateOnly(2024, 11, 3), 5, new TimeOnly(4, 30, 0) },
                    { 12, new DateOnly(2024, 12, 21), 8, new TimeOnly(4, 50, 0) },
                    { 13, new DateOnly(2024, 12, 10), 1, new TimeOnly(4, 10, 0) },
                    { 14, new DateOnly(2024, 12, 8), 5, new TimeOnly(4, 30, 0) },
                    { 15, new DateOnly(2025, 1, 3), 15, new TimeOnly(5, 30, 0) },
                    { 16, new DateOnly(2025, 1, 5), 5, new TimeOnly(3, 30, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RadiationMeasurements");

            migrationBuilder.DropTable(
                name: "Temperatures");
        }
    }
}
