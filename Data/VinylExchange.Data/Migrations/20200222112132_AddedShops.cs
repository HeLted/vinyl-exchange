using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylExchange.Data.Migrations
{
    public partial class AddedShops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ShopType = table.Column<int>(nullable: false),
                    WebAddress = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Town = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    FileType = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopFiles_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopFiles_ShopId",
                table: "ShopFiles",
                column: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopFiles");

            migrationBuilder.DropTable(
                name: "Shops");
        }
    }
}
