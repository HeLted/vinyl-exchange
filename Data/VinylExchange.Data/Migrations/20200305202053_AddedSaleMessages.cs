namespace VinylExchange.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedSaleMessages : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("SaleMessages");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "SaleMessages",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Content = table.Column<string>(maxLength: 150),
                    SaleId = table.Column<Guid>(),
                    UserId = table.Column<Guid>(),
                    CreatedOn = table.Column<DateTime>(),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleMessages", x => x.Id);
                    table.ForeignKey(
                        "FK_SaleMessages_Sales_SaleId",
                        x => x.SaleId,
                        "Sales",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_SaleMessages_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex("IX_SaleMessages_SaleId", "SaleMessages", "SaleId");

            migrationBuilder.CreateIndex("IX_SaleMessages_UserId", "SaleMessages", "UserId");
        }
    }
}