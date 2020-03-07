namespace VinylExchange.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedAddresses : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "VinylExchangeUserId",
                table: "AspNetUserRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VinylExchangeUserId",
                table: "AspNetUserLogins",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VinylExchangeUserId",
                table: "AspNetUserClaims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_VinylExchangeUserId",
                table: "AspNetUserRoles",
                column: "VinylExchangeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_VinylExchangeUserId",
                table: "AspNetUserLogins",
                column: "VinylExchangeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_VinylExchangeUserId",
                table: "AspNetUserClaims",
                column: "VinylExchangeUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_VinylExchangeUserId",
                table: "AspNetUserClaims",
                column: "VinylExchangeUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_VinylExchangeUserId",
                table: "AspNetUserLogins",
                column: "VinylExchangeUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_VinylExchangeUserId",
                table: "AspNetUserRoles",
                column: "VinylExchangeUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_VinylExchangeUserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_VinylExchangeUserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_VinylExchangeUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(name: "IX_AspNetUserRoles_VinylExchangeUserId", table: "AspNetUserRoles");

            migrationBuilder.DropIndex(name: "IX_AspNetUserLogins_VinylExchangeUserId", table: "AspNetUserLogins");

            migrationBuilder.DropIndex(name: "IX_AspNetUserClaims_VinylExchangeUserId", table: "AspNetUserClaims");

            migrationBuilder.DropColumn(name: "VinylExchangeUserId", table: "AspNetUserRoles");

            migrationBuilder.DropColumn(name: "VinylExchangeUserId", table: "AspNetUserLogins");

            migrationBuilder.DropColumn(name: "VinylExchangeUserId", table: "AspNetUserClaims");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(nullable: false),
                                          Country = table.Column<string>(maxLength: 40, nullable: false),
                                          Town = table.Column<string>(maxLength: 40, nullable: false),
                                          PostalCode = table.Column<string>(maxLength: 40, nullable: false),
                                          FullAddress = table.Column<string>(maxLength: 300, nullable: false),
                                          UserId = table.Column<Guid>(nullable: false)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_Addresses", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Addresses_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateIndex(name: "IX_Addresses_UserId", table: "Addresses", column: "UserId");
        }
    }
}