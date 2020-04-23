namespace VinylExchange.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixedTypoInColumnNames : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("SleeveGrade", "Sales");

            migrationBuilder.DropColumn("VinylGrade", "Sales");

            migrationBuilder.AddColumn<int>("SleeveCondition", "Sales", "int", nullable: false, defaultValue: 0);

            migrationBuilder.AddColumn<int>("VinylCondition", "Sales", "int", nullable: false, defaultValue: 0);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("SleeveCondition", "Sales");

            migrationBuilder.DropColumn("VinylCondition", "Sales");

            migrationBuilder.AddColumn<int>("SleeveGrade", "Sales", nullable: false, defaultValue: 0);

            migrationBuilder.AddColumn<int>("VinylGrade", "Sales", nullable: false, defaultValue: 0);
        }
    }
}