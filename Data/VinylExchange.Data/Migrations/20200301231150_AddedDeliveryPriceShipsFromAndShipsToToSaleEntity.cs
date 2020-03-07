namespace VinylExchange.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedDeliveryPriceShipsFromAndShipsToToSaleEntity : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "ShippingPrice", table: "Sales");

            migrationBuilder.DropColumn(name: "ShipsFrom", table: "Sales");

            migrationBuilder.DropColumn(name: "ShipsTo", table: "Sales");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ShippingPrice",
                table: "Sales",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(name: "ShipsFrom", table: "Sales", nullable: true);

            migrationBuilder.AddColumn<string>(name: "ShipsTo", table: "Sales", nullable: true);
        }
    }
}