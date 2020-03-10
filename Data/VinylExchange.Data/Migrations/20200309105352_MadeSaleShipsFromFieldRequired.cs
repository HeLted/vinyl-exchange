namespace VinylExchange.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MadeSaleShipsFromFieldRequired : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShipsFrom",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShipsFrom",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}