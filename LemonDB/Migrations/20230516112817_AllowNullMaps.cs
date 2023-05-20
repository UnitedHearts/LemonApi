using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemonDB.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullMaps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Maps_MapId",
                table: "Sessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "MapId",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Maps_MapId",
                table: "Sessions",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Maps_MapId",
                table: "Sessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "MapId",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Maps_MapId",
                table: "Sessions",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
