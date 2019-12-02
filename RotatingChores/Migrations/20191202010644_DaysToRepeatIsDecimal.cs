using Microsoft.EntityFrameworkCore.Migrations;

namespace RotatingChores.Migrations
{
    public partial class DaysToRepeatIsDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DaysToRepeat",
                table: "Chores",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DaysToRepeat",
                table: "Chores",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
