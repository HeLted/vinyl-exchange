namespace VinylExchange.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemovedShops : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("DeletedOn", "ReleaseFiles");

            migrationBuilder.DropColumn("IsDeleted", "ReleaseFiles");

            migrationBuilder.DropColumn("ModifiedOn", "ReleaseFiles");

            migrationBuilder.AddColumn<Guid>("ShopId", "Sales", "uniqueidentifier", nullable: true);

            migrationBuilder.CreateTable(
                "Shops",
                table => new
                {
                    Id = table.Column<Guid>("uniqueidentifier"),
                    Address = table.Column<string>("nvarchar(max)", nullable: true),
                    Country = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>("datetime2"),
                    DeletedOn = table.Column<DateTime>("datetime2", nullable: true),
                    IsDeleted = table.Column<bool>("bit"),
                    ModifiedOn = table.Column<DateTime>("datetime2", nullable: true),
                    Name = table.Column<string>("nvarchar(max)"),
                    ShopType = table.Column<int>("int"),
                    Town = table.Column<string>("nvarchar(max)", nullable: true),
                    WebAddress = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Shops", x => x.Id); });

            migrationBuilder.CreateTable(
                "ShopFiles",
                table => new
                {
                    Id = table.Column<Guid>("uniqueidentifier"),
                    CreatedOn = table.Column<DateTime>("datetime2"),
                    FileName = table.Column<string>("nvarchar(max)"),
                    FileType = table.Column<int>("int"),
                    Path = table.Column<string>("nvarchar(max)"),
                    ShopId = table.Column<Guid>("uniqueidentifier")
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

            migrationBuilder.CreateIndex("IX_Sales_ShopId", "Sales", "ShopId");

            migrationBuilder.CreateIndex("IX_ShopFiles_ShopId", "ShopFiles", "ShopId");

            migrationBuilder.AddForeignKey(
                "FK_Sales_Shops_ShopId",
                "Sales",
                "ShopId",
                "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Sales_Shops_ShopId", "Sales");

            migrationBuilder.DropTable("ShopFiles");

            migrationBuilder.DropTable("Shops");

            migrationBuilder.DropIndex("IX_Sales_ShopId", "Sales");

            migrationBuilder.DropColumn("ShopId", "Sales");

            migrationBuilder.AddColumn<DateTime>("DeletedOn", "ReleaseFiles", nullable: true);

            migrationBuilder.AddColumn<bool>("IsDeleted", "ReleaseFiles", nullable: false, defaultValue: false);

            migrationBuilder.AddColumn<DateTime>("ModifiedOn", "ReleaseFiles", nullable: true);
        }
    }
}