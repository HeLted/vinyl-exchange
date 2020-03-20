namespace VinylExchange.Data.Migrations
{
    #region

    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    #endregion

    public partial class InitIncludesOldVinylExchangeMigrations : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AspNetRoleClaims");

            migrationBuilder.DropTable("AspNetUserClaims");

            migrationBuilder.DropTable("AspNetUserLogins");

            migrationBuilder.DropTable("AspNetUserRoles");

            migrationBuilder.DropTable("AspNetUserTokens");

            migrationBuilder.DropTable("Collections");

            migrationBuilder.DropTable("DeviceCodes");

            migrationBuilder.DropTable("PersistedGrants");

            migrationBuilder.DropTable("ReleaseFiles");

            migrationBuilder.DropTable("StyleReleases");

            migrationBuilder.DropTable("AspNetRoles");

            migrationBuilder.DropTable("AspNetUsers");

            migrationBuilder.DropTable("Releases");

            migrationBuilder.DropTable("Styles");

            migrationBuilder.DropTable("Genres");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AspNetRoles",
                table => new
                             {
                                 Id = table.Column<Guid>(),
                                 Name = table.Column<string>(maxLength: 256, nullable: true),
                                 NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                                 ConcurrencyStamp = table.Column<string>(nullable: true),
                                 CreatedOn = table.Column<DateTime>(),
                                 ModifiedOn = table.Column<DateTime>(nullable: true),
                                 IsDeleted = table.Column<bool>(),
                                 DeletedOn = table.Column<DateTime>(nullable: true)
                             },
                constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetUsers",
                table => new
                             {
                                 Id = table.Column<Guid>(),
                                 UserName = table.Column<string>(maxLength: 256, nullable: true),
                                 NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                                 Email = table.Column<string>(maxLength: 256, nullable: true),
                                 NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                                 EmailConfirmed = table.Column<bool>(),
                                 PasswordHash = table.Column<string>(nullable: true),
                                 SecurityStamp = table.Column<string>(nullable: true),
                                 ConcurrencyStamp = table.Column<string>(nullable: true),
                                 PhoneNumber = table.Column<string>(nullable: true),
                                 PhoneNumberConfirmed = table.Column<bool>(),
                                 TwoFactorEnabled = table.Column<bool>(),
                                 LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                                 LockoutEnabled = table.Column<bool>(),
                                 AccessFailedCount = table.Column<int>(),
                                 CreatedOn = table.Column<DateTime>(),
                                 ModifiedOn = table.Column<DateTime>(nullable: true),
                                 IsDeleted = table.Column<bool>(),
                                 DeletedOn = table.Column<DateTime>(nullable: true)
                             },
                constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                "DeviceCodes",
                table => new
                             {
                                 UserCode = table.Column<string>(maxLength: 200),
                                 DeviceCode = table.Column<string>(maxLength: 200),
                                 SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                                 ClientId = table.Column<string>(maxLength: 200),
                                 CreationTime = table.Column<DateTime>(),
                                 Expiration = table.Column<DateTime>(),
                                 Data = table.Column<string>(maxLength: 50000)
                             },
                constraints: table => { table.PrimaryKey("PK_DeviceCodes", x => x.UserCode); });

            migrationBuilder.CreateTable(
                "Genres",
                table => new
                             {
                                 Id = table.Column<int>().Annotation("SqlServer:Identity", "1, 1"),
                                 Name = table.Column<string>()
                             },
                constraints: table => { table.PrimaryKey("PK_Genres", x => x.Id); });

            migrationBuilder.CreateTable(
                "PersistedGrants",
                table => new
                             {
                                 Key = table.Column<string>(maxLength: 200),
                                 Type = table.Column<string>(maxLength: 50),
                                 SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                                 ClientId = table.Column<string>(maxLength: 200),
                                 CreationTime = table.Column<DateTime>(),
                                 Expiration = table.Column<DateTime>(nullable: true),
                                 Data = table.Column<string>(maxLength: 50000)
                             },
                constraints: table => { table.PrimaryKey("PK_PersistedGrants", x => x.Key); });

            migrationBuilder.CreateTable(
                "Releases",
                table => new
                             {
                                 Id = table.Column<Guid>(),
                                 Artist = table.Column<string>(),
                                 Title = table.Column<string>(),
                                 Format = table.Column<string>(),
                                 Year = table.Column<string>(),
                                 Label = table.Column<string>()
                             },
                constraints: table => { table.PrimaryKey("PK_Releases", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetRoleClaims",
                table => new
                             {
                                 Id = table.Column<int>().Annotation("SqlServer:Identity", "1, 1"),
                                 RoleId = table.Column<Guid>(),
                                 ClaimType = table.Column<string>(nullable: true),
                                 ClaimValue = table.Column<string>(nullable: true)
                             },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                        table.ForeignKey(
                            "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                            x => x.RoleId,
                            "AspNetRoles",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                "AspNetUserClaims",
                table => new
                             {
                                 Id = table.Column<int>().Annotation("SqlServer:Identity", "1, 1"),
                                 UserId = table.Column<Guid>(),
                                 ClaimType = table.Column<string>(nullable: true),
                                 ClaimValue = table.Column<string>(nullable: true),
                                 VinylExchangeUserId = table.Column<Guid>(nullable: true)
                             },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                        table.ForeignKey(
                            "FK_AspNetUserClaims_AspNetUsers_UserId",
                            x => x.UserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            "FK_AspNetUserClaims_AspNetUsers_VinylExchangeUserId",
                            x => x.VinylExchangeUserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Restrict);
                    });

            migrationBuilder.CreateTable(
                "AspNetUserLogins",
                table => new
                             {
                                 LoginProvider = table.Column<string>(maxLength: 128),
                                 ProviderKey = table.Column<string>(maxLength: 128),
                                 ProviderDisplayName = table.Column<string>(nullable: true),
                                 UserId = table.Column<Guid>(),
                                 VinylExchangeUserId = table.Column<Guid>(nullable: true)
                             },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                        table.ForeignKey(
                            "FK_AspNetUserLogins_AspNetUsers_UserId",
                            x => x.UserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            "FK_AspNetUserLogins_AspNetUsers_VinylExchangeUserId",
                            x => x.VinylExchangeUserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Restrict);
                    });

            migrationBuilder.CreateTable(
                "AspNetUserRoles",
                table => new
                             {
                                 UserId = table.Column<Guid>(),
                                 RoleId = table.Column<Guid>(),
                                 VinylExchangeUserId = table.Column<Guid>(nullable: true)
                             },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                        table.ForeignKey(
                            "FK_AspNetUserRoles_AspNetRoles_RoleId",
                            x => x.RoleId,
                            "AspNetRoles",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            "FK_AspNetUserRoles_AspNetUsers_UserId",
                            x => x.UserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            "FK_AspNetUserRoles_AspNetUsers_VinylExchangeUserId",
                            x => x.VinylExchangeUserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Restrict);
                    });

            migrationBuilder.CreateTable(
                "AspNetUserTokens",
                table => new
                             {
                                 UserId = table.Column<Guid>(),
                                 LoginProvider = table.Column<string>(maxLength: 128),
                                 Name = table.Column<string>(maxLength: 128),
                                 Value = table.Column<string>(nullable: true)
                             },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                        table.ForeignKey(
                            "FK_AspNetUserTokens_AspNetUsers_UserId",
                            x => x.UserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                "Styles",
                table => new
                             {
                                 Id = table.Column<int>().Annotation("SqlServer:Identity", "1, 1"),
                                 Name = table.Column<string>(),
                                 GenreId = table.Column<int>()
                             },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_Styles", x => x.Id);
                        table.ForeignKey(
                            "FK_Styles_Genres_GenreId",
                            x => x.GenreId,
                            "Genres",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                "Collections",
                table => new
                             {
                                 Id = table.Column<Guid>(),
                                 VinylGrade = table.Column<int>(),
                                 SleeveGrade = table.Column<int>(),
                                 Description = table.Column<string>(),
                                 ReleaseId = table.Column<Guid>(),
                                 UserId = table.Column<Guid>()
                             },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_Collections", x => x.Id);
                        table.ForeignKey(
                            "FK_Collections_Releases_ReleaseId",
                            x => x.ReleaseId,
                            "Releases",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            "FK_Collections_AspNetUsers_UserId",
                            x => x.UserId,
                            "AspNetUsers",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                "ReleaseFiles",
                table => new
                             {
                                 Id = table.Column<Guid>(),
                                 Path = table.Column<string>(),
                                 FileName = table.Column<string>(),
                                 FileType = table.Column<int>(),
                                 CreatedOn = table.Column<DateTime>(),
                                 ReleaseId = table.Column<Guid>()
                             },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_ReleaseFiles", x => x.Id);
                        table.ForeignKey(
                            "FK_ReleaseFiles_Releases_ReleaseId",
                            x => x.ReleaseId,
                            "Releases",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                "StyleReleases",
                table => new { StyleId = table.Column<int>(), ReleaseId = table.Column<Guid>() },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_StyleReleases", x => new { x.StyleId, x.ReleaseId });
                        table.ForeignKey(
                            "FK_StyleReleases_Releases_ReleaseId",
                            x => x.ReleaseId,
                            "Releases",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            "FK_StyleReleases_Styles_StyleId",
                            x => x.StyleId,
                            "Styles",
                            "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateIndex("IX_AspNetRoleClaims_RoleId", "AspNetRoleClaims", "RoleId");

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "AspNetRoles",
                "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex("IX_AspNetUserClaims_UserId", "AspNetUserClaims", "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserClaims_VinylExchangeUserId",
                "AspNetUserClaims",
                "VinylExchangeUserId");

            migrationBuilder.CreateIndex("IX_AspNetUserLogins_UserId", "AspNetUserLogins", "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserLogins_VinylExchangeUserId",
                "AspNetUserLogins",
                "VinylExchangeUserId");

            migrationBuilder.CreateIndex("IX_AspNetUserRoles_RoleId", "AspNetUserRoles", "RoleId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserRoles_VinylExchangeUserId",
                "AspNetUserRoles",
                "VinylExchangeUserId");

            migrationBuilder.CreateIndex("EmailIndex", "AspNetUsers", "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "AspNetUsers",
                "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex("IX_Collections_ReleaseId", "Collections", "ReleaseId");

            migrationBuilder.CreateIndex("IX_Collections_UserId", "Collections", "UserId");

            migrationBuilder.CreateIndex("IX_DeviceCodes_DeviceCode", "DeviceCodes", "DeviceCode", unique: true);

            migrationBuilder.CreateIndex("IX_DeviceCodes_Expiration", "DeviceCodes", "Expiration");

            migrationBuilder.CreateIndex("IX_PersistedGrants_Expiration", "PersistedGrants", "Expiration");

            migrationBuilder.CreateIndex(
                "IX_PersistedGrants_SubjectId_ClientId_Type",
                "PersistedGrants",
                new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex("IX_ReleaseFiles_ReleaseId", "ReleaseFiles", "ReleaseId");

            migrationBuilder.CreateIndex("IX_StyleReleases_ReleaseId", "StyleReleases", "ReleaseId");

            migrationBuilder.CreateIndex("IX_Styles_GenreId", "Styles", "GenreId");
        }
    }
}