namespace VinylExchange.Data.Migrations
{
    #region

    using Microsoft.EntityFrameworkCore.Migrations;

    #endregion

    public partial class AddedUserAvatar : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Avatar", table: "AspNetUsers");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "AspNetUsers",
                maxLength: 10000000,
                nullable: false,
                defaultValue: new byte[] { });
        }
    }
}