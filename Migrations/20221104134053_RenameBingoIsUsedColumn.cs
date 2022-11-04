using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bingo.Migrations
{
    public partial class RenameBingoIsUsedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_used",
                table: "bingo");

            migrationBuilder.RenameColumn(
                name: "winner_choosed_at",
                table: "user",
                newName: "bingo_hit_at");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "bingo",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "bingo");

            migrationBuilder.RenameColumn(
                name: "bingo_hit_at",
                table: "user",
                newName: "winner_choosed_at");

            migrationBuilder.AddColumn<bool>(
                name: "is_used",
                table: "bingo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
