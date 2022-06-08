using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniFootballStatistic.Data.Mogrations
{
    public partial class AddPositionResultPropertyInTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositionResult",
                table: "Team",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionResult",
                table: "Team");
        }
    }
}
