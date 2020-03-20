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
            migrationBuilder.DropTable("Sales");

            migrationBuilder.DropTable("ShopFiles");

            migrationBuilder.DropTable("Shops");

            migrationBuilder.DropColumn("CreatedOn", "Releases");

            migrationBuilder.DropColumn("DeletedOn", "Releases");

            migrationBuilder.DropColumn("IsDeleted", "Releases");

            migrationBuilder.DropColumn("ModifiedOn", "Releases");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                "CreatedOn",
                "Releases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>("DeletedOn", "Releases", nullable: true);

            migrationBuilder.AddColumn<bool>("IsDeleted", "Releases", nullable: false, defaultValue: false);

            migrationBuilder.AddColumn<DateTime>("ModifiedOn", "Releases", nullable: true);

            migrationBuilder.CreateTable(
                "Shops",
                table => new
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
                "Sales",
                table => new
                             {
                                 Id = table.Column<Guid>(),
                                 SellerId = table.Column<Guid>(nullable: true),
                                 BuyerId = table.Column<Guid>(nullable: true),
                                 ShopId = table.Column<Guid>(nullable: true),
                                 ReleaseId = table.Column<Guid>(nullable: true),
                                 Status = table.Column<int>(),
                                 Price = table.Column<decimal>("decimal(18,4)"),
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
                            "FK_Sales_AspNetUsers_BuyerId",
                            x => x.BuyerId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Restrict);
                        table.ForeignKey(
                            "FK_Sales_Releases_ReleaseId",
                            x => x.ReleaseId,
                            "Releases",
                            "Id",
                            onDelete: ReferentialAction.Restrict);
                        table.ForeignKey(
                            "FK_Sales_AspNetUsers_SellerId",
                            x => x.SellerId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Restrict);
                        table.ForeignKey(
                            "FK_Sales_Shops_ShopId",
                            x => x.ShopId,
                            "Shops",
                            "Id",
                            onDelete: ReferentialAction.Restrict);
                    });

            migrationBuilder.CreateTable(
                "ShopFiles",
                table => new
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
                            "FK_ShopFiles_Shops_ShopId",
                            x => x.ShopId,
                            "Shops",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateIndex("IX_Sales_BuyerId", "Sales", "BuyerId");

            migrationBuilder.CreateIndex("IX_Sales_ReleaseId", "Sales", "ReleaseId");

            migrationBuilder.CreateIndex("IX_Sales_SellerId", "Sales", "SellerId");

            migrationBuilder.CreateIndex("IX_Sales_ShopId", "Sales", "ShopId");

            migrationBuilder.CreateIndex("IX_ShopFiles_ShopId", "ShopFiles", "ShopId");
        }
    }
}