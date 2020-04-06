namespace VinylExchange.Services.Data.Tests.TestFactories
{
    #region

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;

    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.HelperServices.Releases;
    using VinylExchange.Services.Data.HelperServices.Sales.SaleLogs;
    using VinylExchange.Services.Data.HelperServices.Sales.SaleMessages;
    using VinylExchange.Services.Data.HelperServices.Users;
    using VinylExchange.Services.Data.MainServices.Addresses;
    using VinylExchange.Services.Data.MainServices.Collections;
    using VinylExchange.Services.Data.MainServices.Genres;
    using VinylExchange.Services.Data.MainServices.Releases;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Services.Data.MainServices.Styles;
    using VinylExchange.Services.Data.MainServices.Users;
    using VinylExchange.Services.EmaiSender;
    using VinylExchange.Services.Files;
    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MemoryCache;
    using VinylExchange.Web.Infrastructure.IdentityServer.Profile;

    #endregion

    internal static class MockingDependenciesFactory
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddDbContext<VinylExchangeDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddIdentityCore<VinylExchangeUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<VinylExchangeRole>().AddEntityFrameworkStores<VinylExchangeDbContext>().AddSignInManager();

            services.Configure<IdentityOptions>(
                options =>
                    {
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.SignIn.RequireConfirmedAccount = false;
                        options.SignIn.RequireConfirmedEmail = false;
                        options.User.RequireUniqueEmail = true;
                    });
            services.AddAuthentication(
                options =>
                    {
                        // ...
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                    }).AddJwtBearer(
                options =>
                    {
                        // ...
                        options.SecurityTokenValidators.Clear();
                        options.SecurityTokenValidators.Add(
                            new JwtSecurityTokenHandler
                                {
                                    // Disable the built-in JWT claims mapping feature.
                                    InboundClaimTypeMap = new Dictionary<string, string>()
                                });
                    }).AddIdentityServerJwt();

            services.AddIdentityServer().AddApiAuthorization<VinylExchangeUser, VinylExchangeDbContext>()
                .AddProfileService<ProfileService>();

            services.AddDistributedMemoryCache();

            // Enitity Services
            services.AddTransient<IReleasesService, ReleasesService>();
            services.AddTransient<IGenresService, GenresService>();
            services.AddTransient<IStylesService, StylesService>();
            services.AddTransient<IReleaseFilesService, ReleaseFilesService>();
            services.AddTransient<ICollectionsService, CollectionsService>();
            services.AddTransient<IAddressesService, AddressesService>();
            services.AddTransient<IUsersAvatarService, UsersAvatarService>();
            services.AddTransient<ISalesService, SalesService>();
            services.AddTransient<ISaleMessagesService, SaleMessagesService>();
            services.AddTransient<ISaleLogsService, SaleLogsService>();
            services.AddTransient<IUsersService, UsersService>();

            // Entity Retrievers
            services.AddTransient<IUsersEntityRetriever, UsersService>();
            services.AddTransient<IAddressesEntityRetriever, AddressesService>();
            services.AddTransient<IReleasesEntityRetriever, ReleasesService>();

            // Tool Services
            services.AddTransient<IMemoryCache, MemoryCache>();
            services.AddTransient<MemoryCacheManager>();
            services.AddTransient<IMemoryCacheFileSevice, MemoryCacheFileService>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddSingleton<IEmailSender>(new EmailSender("Test API Key"));
        }
    }
}