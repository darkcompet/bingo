using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bingo.Migrations
{
    public partial class RenameBingoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mst_bingo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    is_used = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_bingo", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mst_bingo_code",
                table: "mst_bingo",
                column: "code",
                unique: true,
                filter: "[code] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mst_bingo");
        }
    }
}
