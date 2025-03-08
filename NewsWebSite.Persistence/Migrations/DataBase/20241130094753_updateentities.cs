using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsWebSite.Persistence.Migrations.DataBase
{
    public partial class updateentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RateCount",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LikeNews_UserId",
                table: "LikeNews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeNews_Users_UserId",
                table: "LikeNews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeNews_Users_UserId",
                table: "LikeNews");

            migrationBuilder.DropIndex(
                name: "IX_LikeNews_UserId",
                table: "LikeNews");

            migrationBuilder.DropColumn(
                name: "RateCount",
                table: "News");
        }
    }
}
