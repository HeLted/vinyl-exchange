namespace VinylExchange.Data.Migrations
{
    #region

    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    #endregion

    public partial class AddedSaleLogs : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("SaleLogs");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "SaleLogs",
                table => new
                    {
                        Id = table.Column<Guid>(),
                        CreatedOn = table.Column<DateTime>(),
                        ModifiedOn = table.Column<DateTime>(nullable: true),
                        Content = table.Column<string>(),
                        SaleId = table.Column<Guid>()
                    },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_SaleLogs", x => x.Id);
                        table.ForeignKey(
                            "FK_SaleLogs_Sales_SaleId",
                            x => x.SaleId,
                            "Sales",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateIndex("IX_SaleLogs_SaleId", "SaleLogs", "SaleId");
        }
    }
}