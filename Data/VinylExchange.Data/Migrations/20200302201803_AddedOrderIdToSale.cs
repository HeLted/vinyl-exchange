namespace VinylExchange.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

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