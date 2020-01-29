using Microsoft.EntityFrameworkCore.Migrations;

namespace RotatingChores.Migrations
{
    public partial class CustomUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Chores",
                newName: "RotatingChoresUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Chores_RotatingChoresUserID",
                table: "Chores",
                column: "RotatingChoresUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Chores_AspNetUsers_RotatingChoresUserID",
                table: "Chores",
                column: "RotatingChoresUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chores_AspNetUsers_RotatingChoresUserID",
                table: "Chores");

            migrationBuilder.DropIndex(
                name: "IX_Chores_RotatingChoresUserID",
                table: "Chores");

            migrationBuilder.RenameColumn(
                name: "RotatingChoresUserID",
                table: "Chores",
                newName: "UserID");
        }
    }
}
