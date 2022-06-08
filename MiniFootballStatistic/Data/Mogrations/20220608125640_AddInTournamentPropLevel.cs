using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniFootballStatistic.Data.Mogrations
{
    public partial class AddInTournamentPropLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Levels",
                table: "Tournament",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Levels",
                table: "Tournament");
        }
    }
}
