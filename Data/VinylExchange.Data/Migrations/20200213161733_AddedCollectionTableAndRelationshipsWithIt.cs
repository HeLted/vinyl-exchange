using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylExchange.Data.Migrations
{
    public partial class AddedCollectionTableAndRelationshipsWithIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReleaseId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collection_Releases_ReleaseId",
                        column: x => x.ReleaseId,
                        principalTable: "Releases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collection_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collection_ReleaseId",
                table: "Collection",
                column: "ReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_UserId",
                table: "Collection",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collection");
        }
    }
}
