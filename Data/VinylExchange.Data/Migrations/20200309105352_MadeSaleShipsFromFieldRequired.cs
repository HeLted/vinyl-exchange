namespace VinylExchange.Data.Migrations
{
    #region

    using Microsoft.EntityFrameworkCore.Migrations;

    #endregion

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