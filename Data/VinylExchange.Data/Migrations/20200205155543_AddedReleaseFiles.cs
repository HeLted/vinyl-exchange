using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylExchange.Data.Migrations
{
    public partial class AddedReleaseFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReleaseFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ReleaseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseFiles_Releases_ReleaseId",
                        column: x => x.ReleaseId,
                        principalTable: "Releases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseFiles_ReleaseId",
                table: "ReleaseFiles",
                column: "ReleaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReleaseFiles");
        }
    }
}
