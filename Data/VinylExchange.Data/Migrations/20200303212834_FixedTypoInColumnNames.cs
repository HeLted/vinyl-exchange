namespace VinylExchange.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixedTypoInColumnNames : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "SleeveGrade", table: "Sales");

            migrationBuilder.DropColumn(name: "VinylGrade", table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "SleeveCondition",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VinylCondition",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "SleeveCondition", table: "Sales");

            migrationBuilder.DropColumn(name: "VinylCondition", table: "Sales");

            migrationBuilder.AddColumn<int>(name: "SleeveGrade", table: "Sales", nullable: false, defaultValue: 0);

            migrationBuilder.AddColumn<int>(name: "VinylGrade", table: "Sales", nullable: false, defaultValue: 0);
        }
    }
}