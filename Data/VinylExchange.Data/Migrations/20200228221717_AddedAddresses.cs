namespace VinylExchange.Data.Migrations
{
    #region

    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    #endregion

    public partial class AddedAddresses : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Addresses");

            migrationBuilder.AddColumn<Guid>(
                "VinylExchangeUserId",
                "AspNetUserRoles",
                "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                "VinylExchangeUserId",
                "AspNetUserLogins",
                "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                "VinylExchangeUserId",
                "AspNetUserClaims",
                "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_AspNetUserRoles_VinylExchangeUserId",
                "AspNetUserRoles",
                "VinylExchangeUserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserLogins_VinylExchangeUserId",
                "AspNetUserLogins",
                "VinylExchangeUserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserClaims_VinylExchangeUserId",
                "AspNetUserClaims",
                "VinylExchangeUserId");

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserClaims_AspNetUsers_VinylExchangeUserId",
                "AspNetUserClaims",
                "VinylExchangeUserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserLogins_AspNetUsers_VinylExchangeUserId",
                "AspNetUserLogins",
                "VinylExchangeUserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserRoles_AspNetUsers_VinylExchangeUserId",
                "AspNetUserRoles",
                "VinylExchangeUserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_AspNetUserClaims_AspNetUsers_VinylExchangeUserId", "AspNetUserClaims");

            migrationBuilder.DropForeignKey("FK_AspNetUserLogins_AspNetUsers_VinylExchangeUserId", "AspNetUserLogins");

            migrationBuilder.DropForeignKey("FK_AspNetUserRoles_AspNetUsers_VinylExchangeUserId", "AspNetUserRoles");

            migrationBuilder.DropIndex("IX_AspNetUserRoles_VinylExchangeUserId", "AspNetUserRoles");

            migrationBuilder.DropIndex("IX_AspNetUserLogins_VinylExchangeUserId", "AspNetUserLogins");

            migrationBuilder.DropIndex("IX_AspNetUserClaims_VinylExchangeUserId", "AspNetUserClaims");

            migrationBuilder.DropColumn("VinylExchangeUserId", "AspNetUserRoles");

            migrationBuilder.DropColumn("VinylExchangeUserId", "AspNetUserLogins");

            migrationBuilder.DropColumn("VinylExchangeUserId", "AspNetUserClaims");

            migrationBuilder.CreateTable(
                "Addresses",
                table => new
                             {
                                 Id = table.Column<Guid>(),
                                 Country = table.Column<string>(maxLength: 40),
                                 Town = table.Column<string>(maxLength: 40),
                                 PostalCode = table.Column<string>(maxLength: 40),
                                 FullAddress = table.Column<string>(maxLength: 300),
                                 UserId = table.Column<Guid>()
                             },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_Addresses", x => x.Id);
                        table.ForeignKey(
                            "FK_Addresses_AspNetUsers_UserId",
                            x => x.UserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateIndex("IX_Addresses_UserId", "Addresses", "UserId");
        }
    }
}