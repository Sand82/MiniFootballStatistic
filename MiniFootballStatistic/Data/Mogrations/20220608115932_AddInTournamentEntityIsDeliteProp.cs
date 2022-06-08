using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniFootballStatistic.Data.Mogrations
{
    public partial class AddInTournamentEntityIsDeliteProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "Tournament",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "Tournament");
        }
    }
}
