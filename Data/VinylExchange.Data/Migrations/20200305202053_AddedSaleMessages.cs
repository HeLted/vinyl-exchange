namespace VinylExchange.Data.Migrations
{
    #region

    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    #endregion

    public partial class AddedSaleMessages : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "SaleMessages");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaleMessages",
                columns: table => new
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
                            name: "FK_SaleMessages_Sales_SaleId",
                            column: x => x.SaleId,
                            principalTable: "Sales",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_SaleMessages_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateIndex(name: "IX_SaleMessages_SaleId", table: "SaleMessages", column: "SaleId");

            migrationBuilder.CreateIndex(name: "IX_SaleMessages_UserId", table: "SaleMessages", column: "UserId");
        }
    }
}