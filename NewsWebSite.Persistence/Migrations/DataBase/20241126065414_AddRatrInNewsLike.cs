using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsWebSite.Persistence.Migrations.DataBase
{
    public partial class AddRatrInNewsLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Rate",
                table: "LikeNews",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "LikeNews");
        }
    }
}
