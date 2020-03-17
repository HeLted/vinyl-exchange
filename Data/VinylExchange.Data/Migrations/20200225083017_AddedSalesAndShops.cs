namespace VinylExchange.Data.Migrations
{
    #region

    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    #endregion

    public partial class AddedSalesAndShops : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Sales");

            migrationBuilder.DropTable(name: "ShopFiles");

            migrationBuilder.DropTable(name: "Shops");

            migrationBuilder.DropColumn(name: "CreatedOn", table: "Releases");

            migrationBuilder.DropColumn(name: "DeletedOn", table: "Releases");

            migrationBuilder.DropColumn(name: "IsDeleted", table: "Releases");

            migrationBuilder.DropColumn(name: "ModifiedOn", table: "Releases");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Releases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(name: "DeletedOn", table: "Releases", nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Releases",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(name: "ModifiedOn", table: "Releases", nullable: true);

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(),
                                          Name = table.Column<string>(),
                                          ShopType = table.Column<int>(),
                                          WebAddress = table.Column<string>(nullable: true),
                                          Country = table.Column<string>(nullable: true),
                                          Town = table.Column<string>(nullable: true),
                                          Address = table.Column<string>(nullable: true),
                                          CreatedOn = table.Column<DateTime>(),
                                          ModifiedOn = table.Column<DateTime>(nullable: true),
                                          IsDeleted = table.Column<bool>(),
                                          DeletedOn = table.Column<DateTime>(nullable: true)
                                      },
                constraints: table => { table.PrimaryKey("PK_Shops", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(),
                                          SellerId = table.Column<Guid>(nullable: true),
                                          BuyerId = table.Column<Guid>(nullable: true),
                                          ShopId = table.Column<Guid>(nullable: true),
                                          ReleaseId = table.Column<Guid>(nullable: true),
                                          Status = table.Column<int>(),
                                          Price = table.Column<decimal>(type: "decimal(18,4)"),
                                          VinylCondition = table.Column<int>(),
                                          SleeveCondition = table.Column<int>(),
                                          Description = table.Column<string>(),
                                          CreatedOn = table.Column<DateTime>(),
                                          ModifiedOn = table.Column<DateTime>(nullable: true),
                                          IsDeleted = table.Column<bool>(),
                                          DeletedOn = table.Column<DateTime>(nullable: true)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_Sales", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Sales_AspNetUsers_BuyerId",
                            column: x => x.BuyerId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
                        table.ForeignKey(
                            name: "FK_Sales_Releases_ReleaseId",
                            column: x => x.ReleaseId,
                            principalTable: "Releases",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
                        table.ForeignKey(
                            name: "FK_Sales_AspNetUsers_SellerId",
                            column: x => x.SellerId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
                        table.ForeignKey(
                            name: "FK_Sales_Shops_ShopId",
                            column: x => x.ShopId,
                            principalTable: "Shops",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
                    });

            migrationBuilder.CreateTable(
                name: "ShopFiles",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(),
                                          Path = table.Column<string>(),
                                          FileName = table.Column<string>(),
                                          FileType = table.Column<int>(),
                                          CreatedOn = table.Column<DateTime>(),
                                          ShopId = table.Column<Guid>()
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

            migrationBuilder.CreateIndex(name: "IX_Sales_BuyerId", table: "Sales", column: "BuyerId");

            migrationBuilder.CreateIndex(name: "IX_Sales_ReleaseId", table: "Sales", column: "ReleaseId");

            migrationBuilder.CreateIndex(name: "IX_Sales_SellerId", table: "Sales", column: "SellerId");

            migrationBuilder.CreateIndex(name: "IX_Sales_ShopId", table: "Sales", column: "ShopId");

            migrationBuilder.CreateIndex(name: "IX_ShopFiles_ShopId", table: "ShopFiles", column: "ShopId");
        }
    }
}