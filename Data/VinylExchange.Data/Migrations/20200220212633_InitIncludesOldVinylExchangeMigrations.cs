namespace VinylExchange.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InitIncludesOldVinylExchangeMigrations : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AspNetRoleClaims");

            migrationBuilder.DropTable(name: "AspNetUserClaims");

            migrationBuilder.DropTable(name: "AspNetUserLogins");

            migrationBuilder.DropTable(name: "AspNetUserRoles");

            migrationBuilder.DropTable(name: "AspNetUserTokens");

            migrationBuilder.DropTable(name: "Collections");

            migrationBuilder.DropTable(name: "DeviceCodes");

            migrationBuilder.DropTable(name: "PersistedGrants");

            migrationBuilder.DropTable(name: "ReleaseFiles");

            migrationBuilder.DropTable(name: "StyleReleases");

            migrationBuilder.DropTable(name: "AspNetRoles");

            migrationBuilder.DropTable(name: "AspNetUsers");

            migrationBuilder.DropTable(name: "Releases");

            migrationBuilder.DropTable(name: "Styles");

            migrationBuilder.DropTable(name: "Genres");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(nullable: false),
                                          Name = table.Column<string>(maxLength: 256, nullable: true),
                                          NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                                          ConcurrencyStamp = table.Column<string>(nullable: true),
                                          CreatedOn = table.Column<DateTime>(nullable: false),
                                          ModifiedOn = table.Column<DateTime>(nullable: true),
                                          IsDeleted = table.Column<bool>(nullable: false),
                                          DeletedOn = table.Column<DateTime>(nullable: true)
                                      },
                constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(nullable: false),
                                          UserName = table.Column<string>(maxLength: 256, nullable: true),
                                          NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                                          Email = table.Column<string>(maxLength: 256, nullable: true),
                                          NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                                          EmailConfirmed = table.Column<bool>(nullable: false),
                                          PasswordHash = table.Column<string>(nullable: true),
                                          SecurityStamp = table.Column<string>(nullable: true),
                                          ConcurrencyStamp = table.Column<string>(nullable: true),
                                          PhoneNumber = table.Column<string>(nullable: true),
                                          PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                                          TwoFactorEnabled = table.Column<bool>(nullable: false),
                                          LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                                          LockoutEnabled = table.Column<bool>(nullable: false),
                                          AccessFailedCount = table.Column<int>(nullable: false),
                                          CreatedOn = table.Column<DateTime>(nullable: false),
                                          ModifiedOn = table.Column<DateTime>(nullable: true),
                                          IsDeleted = table.Column<bool>(nullable: false),
                                          DeletedOn = table.Column<DateTime>(nullable: true)
                                      },
                constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                                      {
                                          UserCode = table.Column<string>(maxLength: 200, nullable: false),
                                          DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                                          SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                                          ClientId = table.Column<string>(maxLength: 200, nullable: false),
                                          CreationTime = table.Column<DateTime>(nullable: false),
                                          Expiration = table.Column<DateTime>(nullable: false),
                                          Data = table.Column<string>(maxLength: 50000, nullable: false)
                                      },
                constraints: table => { table.PrimaryKey("PK_DeviceCodes", x => x.UserCode); });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                                      {
                                          Id = table.Column<int>(nullable: false).Annotation(
                                              "SqlServer:Identity",
                                              "1, 1"),
                                          Name = table.Column<string>(nullable: false)
                                      },
                constraints: table => { table.PrimaryKey("PK_Genres", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                                      {
                                          Key = table.Column<string>(maxLength: 200, nullable: false),
                                          Type = table.Column<string>(maxLength: 50, nullable: false),
                                          SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                                          ClientId = table.Column<string>(maxLength: 200, nullable: false),
                                          CreationTime = table.Column<DateTime>(nullable: false),
                                          Expiration = table.Column<DateTime>(nullable: true),
                                          Data = table.Column<string>(maxLength: 50000, nullable: false)
                                      },
                constraints: table => { table.PrimaryKey("PK_PersistedGrants", x => x.Key); });

            migrationBuilder.CreateTable(
                name: "Releases",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(nullable: false),
                                          Artist = table.Column<string>(nullable: false),
                                          Title = table.Column<string>(nullable: false),
                                          Format = table.Column<string>(nullable: false),
                                          Year = table.Column<string>(nullable: false),
                                          Label = table.Column<string>(nullable: false)
                                      },
                constraints: table => { table.PrimaryKey("PK_Releases", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                                      {
                                          Id = table.Column<int>(nullable: false).Annotation(
                                              "SqlServer:Identity",
                                              "1, 1"),
                                          RoleId = table.Column<Guid>(nullable: false),
                                          ClaimType = table.Column<string>(nullable: true),
                                          ClaimValue = table.Column<string>(nullable: true)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                        table.ForeignKey(
                            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                            column: x => x.RoleId,
                            principalTable: "AspNetRoles",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                                      {
                                          Id = table.Column<int>(nullable: false).Annotation(
                                              "SqlServer:Identity",
                                              "1, 1"),
                                          UserId = table.Column<Guid>(nullable: false),
                                          ClaimType = table.Column<string>(nullable: true),
                                          ClaimValue = table.Column<string>(nullable: true),
                                          VinylExchangeUserId = table.Column<Guid>(nullable: true)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                        table.ForeignKey(
                            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_AspNetUserClaims_AspNetUsers_VinylExchangeUserId",
                            column: x => x.VinylExchangeUserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
                    });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                                      {
                                          LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                                          ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                                          ProviderDisplayName = table.Column<string>(nullable: true),
                                          UserId = table.Column<Guid>(nullable: false),
                                          VinylExchangeUserId = table.Column<Guid>(nullable: true)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                        table.ForeignKey(
                            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_AspNetUserLogins_AspNetUsers_VinylExchangeUserId",
                            column: x => x.VinylExchangeUserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
                    });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                                      {
                                          UserId = table.Column<Guid>(nullable: false),
                                          RoleId = table.Column<Guid>(nullable: false),
                                          VinylExchangeUserId = table.Column<Guid>(nullable: true)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                        table.ForeignKey(
                            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                            column: x => x.RoleId,
                            principalTable: "AspNetRoles",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_AspNetUserRoles_AspNetUsers_VinylExchangeUserId",
                            column: x => x.VinylExchangeUserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
                    });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                                      {
                                          UserId = table.Column<Guid>(nullable: false),
                                          LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                                          Name = table.Column<string>(maxLength: 128, nullable: false),
                                          Value = table.Column<string>(nullable: true)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                        table.ForeignKey(
                            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                                      {
                                          Id = table.Column<int>(nullable: false).Annotation(
                                              "SqlServer:Identity",
                                              "1, 1"),
                                          Name = table.Column<string>(nullable: false),
                                          GenreId = table.Column<int>(nullable: false)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_Styles", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Styles_Genres_GenreId",
                            column: x => x.GenreId,
                            principalTable: "Genres",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(nullable: false),
                                          VinylGrade = table.Column<int>(nullable: false),
                                          SleeveGrade = table.Column<int>(nullable: false),
                                          Description = table.Column<string>(nullable: false),
                                          ReleaseId = table.Column<Guid>(nullable: false),
                                          UserId = table.Column<Guid>(nullable: false)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_Collections", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Collections_Releases_ReleaseId",
                            column: x => x.ReleaseId,
                            principalTable: "Releases",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_Collections_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                name: "ReleaseFiles",
                columns: table => new
                                      {
                                          Id = table.Column<Guid>(nullable: false),
                                          Path = table.Column<string>(nullable: false),
                                          FileName = table.Column<string>(nullable: false),
                                          FileType = table.Column<int>(nullable: false),
                                          CreatedOn = table.Column<DateTime>(nullable: false),
                                          ReleaseId = table.Column<Guid>(nullable: false)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_ReleaseFiles", x => x.Id);
                        table.ForeignKey(
                            name: "FK_ReleaseFiles_Releases_ReleaseId",
                            column: x => x.ReleaseId,
                            principalTable: "Releases",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                name: "StyleReleases",
                columns: table => new
                                      {
                                          StyleId = table.Column<int>(nullable: false),
                                          ReleaseId = table.Column<Guid>(nullable: false)
                                      },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_StyleReleases", x => new { x.StyleId, x.ReleaseId });
                        table.ForeignKey(
                            name: "FK_StyleReleases_Releases_ReleaseId",
                            column: x => x.ReleaseId,
                            principalTable: "Releases",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_StyleReleases_Styles_StyleId",
                            column: x => x.StyleId,
                            principalTable: "Styles",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_VinylExchangeUserId",
                table: "AspNetUserClaims",
                column: "VinylExchangeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_VinylExchangeUserId",
                table: "AspNetUserLogins",
                column: "VinylExchangeUserId");

            migrationBuilder.CreateIndex(name: "IX_AspNetUserRoles_RoleId", table: "AspNetUserRoles", column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_VinylExchangeUserId",
                table: "AspNetUserRoles",
                column: "VinylExchangeUserId");

            migrationBuilder.CreateIndex(name: "EmailIndex", table: "AspNetUsers", column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(name: "IX_Collections_ReleaseId", table: "Collections", column: "ReleaseId");

            migrationBuilder.CreateIndex(name: "IX_Collections_UserId", table: "Collections", column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(name: "IX_DeviceCodes_Expiration", table: "DeviceCodes", column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(name: "IX_ReleaseFiles_ReleaseId", table: "ReleaseFiles", column: "ReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_StyleReleases_ReleaseId",
                table: "StyleReleases",
                column: "ReleaseId");

            migrationBuilder.CreateIndex(name: "IX_Styles_GenreId", table: "Styles", column: "GenreId");
        }
    }
}