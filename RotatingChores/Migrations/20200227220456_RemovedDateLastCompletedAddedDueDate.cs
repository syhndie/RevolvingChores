using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RotatingChores.Migrations
{
    public partial class RemovedDateLastCompletedAddedDueDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLastCompleted",
                table: "Chores");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Chores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Chores");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastCompleted",
                table: "Chores",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
