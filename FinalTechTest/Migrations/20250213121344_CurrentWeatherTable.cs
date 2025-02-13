using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinalTechTest.Migrations
{
    /// <inheritdoc />
    public partial class CurrentWeatherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WindDeg",
                table: "DailyForecasts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "CurrentWeather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Sunrise = table.Column<long>(type: "bigint", nullable: false),
                    Sunset = table.Column<long>(type: "bigint", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    FeelsLike = table.Column<double>(type: "double precision", nullable: false),
                    Pressure = table.Column<int>(type: "integer", nullable: false),
                    Humidity = table.Column<int>(type: "integer", nullable: false),
                    DewPoint = table.Column<double>(type: "double precision", nullable: false),
                    UvIndex = table.Column<double>(type: "double precision", nullable: false),
                    CloudCover = table.Column<int>(type: "integer", nullable: false),
                    Visibility = table.Column<int>(type: "integer", nullable: false),
                    WindSpeed = table.Column<double>(type: "double precision", nullable: false),
                    WindDeg = table.Column<double>(type: "double precision", nullable: false),
                    WindGust = table.Column<double>(type: "double precision", nullable: true),
                    Weather = table.Column<string>(type: "text", nullable: false),
                    WeatherForecastId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentWeather", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentWeather_WeatherForecasts_WeatherForecastId",
                        column: x => x.WeatherForecastId,
                        principalTable: "WeatherForecasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentWeather_WeatherForecastId",
                table: "CurrentWeather",
                column: "WeatherForecastId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentWeather");

            migrationBuilder.DropColumn(
                name: "WindDeg",
                table: "DailyForecasts");
        }
    }
}
