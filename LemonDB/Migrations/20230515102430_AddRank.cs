using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemonDB.Migrations
{
    /// <inheritdoc />
    public partial class AddRank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "PlayersSessionsStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "PlayersSessionsStats");
        }
    }
}
