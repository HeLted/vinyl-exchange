namespace VinylExchange.Data.Migrations
{
    #region

    using Microsoft.EntityFrameworkCore.Migrations;

    #endregion

    public partial class AddedOrderIdToSale : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "OrderId", table: "Sales");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(name: "OrderId", table: "Sales", nullable: true);
        }
    }
}