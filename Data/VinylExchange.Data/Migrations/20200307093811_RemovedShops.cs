namespace VinylExchange.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemovedShops : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "DeletedOn", table: "ReleaseFiles");

            migrationBuilder.DropColumn(name: "IsDeleted", table: "ReleaseFiles");

            migrationBuilder.DropColumn(name: "ModifiedOn", table: "ReleaseFiles");

            migrationBuilder.AddColumn<Guid>(name: "ShopId", table: "Sales", type: "uniqueidentifier", nullable: true);

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                                          Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                          Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                          CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                                          DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                                          IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                                          ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                                          Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                                          ShopType = table.Column<int>(type: "int", nullable: false),
                                          Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                          WebAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                                      },
                constraints: table => { table.PrimaryKey("PK_Shops", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "ShopFiles",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                                          CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                                          FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                                          FileType = table.Column<int>(type: "int", nullable: false),
                                          Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                                          ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(name: "IX_Sales_ShopId", table: "Sales", column: "ShopId");

            migrationBuilder.CreateIndex(name: "IX_ShopFiles_ShopId", table: "ShopFiles", column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Shops_ShopId",
                table: "Sales",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Sales_Shops_ShopId", table: "Sales");

            migrationBuilder.DropTable(name: "ShopFiles");

            migrationBuilder.DropTable(name: "Shops");

            migrationBuilder.DropIndex(name: "IX_Sales_ShopId", table: "Sales");

            migrationBuilder.DropColumn(name: "ShopId", table: "Sales");

            migrationBuilder.AddColumn<DateTime>(name: "DeletedOn", table: "ReleaseFiles", nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ReleaseFiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(name: "ModifiedOn", table: "ReleaseFiles", nullable: true);
        }
    }
}