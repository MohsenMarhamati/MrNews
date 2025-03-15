using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsWebSite.Persistence.Migrations.DataBase
{
    public partial class AddRootCommentIdInComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RootCommentId",
                table: "Comment",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RootCommentId",
                table: "Comment");
        }
    }
}
