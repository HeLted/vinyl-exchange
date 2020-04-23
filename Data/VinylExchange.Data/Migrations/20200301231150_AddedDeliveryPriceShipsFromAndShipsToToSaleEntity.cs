namespace VinylExchange.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedDeliveryPriceShipsFromAndShipsToToSaleEntity : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("ShippingPrice", "Sales");

            migrationBuilder.DropColumn("ShipsFrom", "Sales");

            migrationBuilder.DropColumn("ShipsTo", "Sales");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                "ShippingPrice",
                "Sales",
                "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>("ShipsFrom", "Sales", nullable: true);

            migrationBuilder.AddColumn<string>("ShipsTo", "Sales", nullable: true);
        }
    }
}