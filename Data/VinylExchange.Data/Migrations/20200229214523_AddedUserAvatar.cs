namespace VinylExchange.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedUserAvatar : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Avatar", "AspNetUsers");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                "Avatar",
                "AspNetUsers",
                maxLength: 10000000,
                nullable: false,
                defaultValue: new byte[] { });
        }
    }
}