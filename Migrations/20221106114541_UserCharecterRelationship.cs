using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    public partial class UserCharecterRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Charecters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charecters_UserId",
                table: "Charecters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charecters_Users_UserId",
                table: "Charecters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charecters_Users_UserId",
                table: "Charecters");

            migrationBuilder.DropIndex(
                name: "IX_Charecters_UserId",
                table: "Charecters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Charecters");
        }
    }
}
