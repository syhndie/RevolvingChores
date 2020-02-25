using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RotatingChores.Migrations
{
    public partial class AddedDateModifiedToChore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModiied",
                table: "Chores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLastModiied",
                table: "Chores");
        }
    }
}
