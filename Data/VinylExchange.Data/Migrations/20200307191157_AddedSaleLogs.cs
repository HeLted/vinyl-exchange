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
            migrationBuilder.DropTable(name: "SaleLogs");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaleLogs",
                columns: table => new
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
                            name: "FK_SaleLogs_Sales_SaleId",
                            column: x => x.SaleId,
                            principalTable: "Sales",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateIndex(name: "IX_SaleLogs_SaleId", table: "SaleLogs", column: "SaleId");
        }
    }
}