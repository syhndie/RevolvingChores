using Microsoft.EntityFrameworkCore.Migrations;

namespace RotatingChores.Migrations
{
    public partial class UniqueChoreName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Chores_RotatingChoresUserID",
                table: "Chores");

            migrationBuilder.CreateIndex(
                name: "IX_Chores_RotatingChoresUserID_Name",
                table: "Chores",
                columns: new[] { "RotatingChoresUserID", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Chores_RotatingChoresUserID_Name",
                table: "Chores");

            migrationBuilder.CreateIndex(
                name: "IX_Chores_RotatingChoresUserID",
                table: "Chores",
                column: "RotatingChoresUserID");
        }
    }
}
