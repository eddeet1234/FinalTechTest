using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalTechTest.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtToDailyForecast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DailyForecasts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DailyForecasts");
        }
    }
}
