using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsWebSite.Persistence.Migrations.DataBase
{
    public partial class AddRatrInNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "News",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "News");
        }
    }
}
