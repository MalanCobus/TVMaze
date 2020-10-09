using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TvMaze.Data.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Castmember",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Castmember", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TVShows",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CastShowLinkage",
                columns: table => new
                {
                    TVShowId = table.Column<int>(nullable: false),
                    CastmemberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastShowLinkage", x => new { x.TVShowId, x.CastmemberId });
                    table.ForeignKey(
                        name: "FK_CastShowLinkage_Castmember_CastmemberId",
                        column: x => x.CastmemberId,
                        principalTable: "Castmember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastShowLinkage_TVShows_TVShowId",
                        column: x => x.TVShowId,
                        principalTable: "TVShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastShowLinkage_CastmemberId",
                table: "CastShowLinkage",
                column: "CastmemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastShowLinkage");

            migrationBuilder.DropTable(
                name: "Castmember");

            migrationBuilder.DropTable(
                name: "TVShows");
        }
    }
}
