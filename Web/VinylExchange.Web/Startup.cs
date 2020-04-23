namespace VinylExchange.Web
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Reflection;
    using Data;
    using Data.Models;
    using Data.Seeding;
    using Infrastructure.Hubs.SaleChat;
    using Infrastructure.Hubs.SaleLog;
    using Infrastructure.IdentityServer.Profile;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;
    using Models;
    using Services.Data.HelperServices.Releases;
    using Services.Data.HelperServices.Sales.SaleLogs;
    using Services.Data.HelperServices.Sales.SaleMessages;
    using Services.Data.HelperServices.Users;
    using Services.Data.MainServices.Addresses;
    using Services.Data.MainServices.Addresses.Contracts;
    using Services.Data.MainServices.Collections;
    using Services.Data.MainServices.Genres;
    using Services.Data.MainServices.Genres.Contracts;
    using Services.Data.MainServices.Releases;
    using Services.Data.MainServices.Releases.Contracts;
    using Services.Data.MainServices.Sales;
    using Services.Data.MainServices.Sales.Contracts;
    using Services.Data.MainServices.Styles;
    using Services.Data.MainServices.Users;
    using Services.Data.MainServices.Users.Contracts;
    using Services.EmailSender;
    using Services.Files;
    using Services.Logging;
    using Services.Mapping;
    using Services.MemoryCache;
    using Services.MemoryCache.Contracts;

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<VinylExchangeDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new VinylExchangeDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter()
                    .GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ModelGetAssemblyClass).GetTypeInfo().Assembly);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseStaticFiles();
            app.UseFileServer(
                new FileServerOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "MediaStorage")),
                    RequestPath = "/File/Media",
                    EnableDirectoryBrowsing = true
                });

            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute("Default", "api/{controller}/{id}");
                    endpoints.MapHub<SaleChatHub>("/Sale/Chathub");
                    endpoints.MapHub<SaleLogsHub>("/Sale/LogHub");
                });

            app.Use(
                next => context =>
                {
                    var path = context.Request.Path.Value;

                    if (string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(
                            path,
                            "/Authentication/Logout-Callback",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        var tokens = antiforgery.GetAndStoreTokens(context);
                        context.Response.Cookies.Append(
                            "XSRF-TOKEN",
                            tokens.RequestToken,
                            new CookieOptions {HttpOnly = false});
                    }

                    return next(context);
                });

            app.UseSpa(
                spa =>
                {
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseReactDevelopmentServer("start");
                    }
                });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VinylExchangeDbContext>(
                options => { options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")); });

            services.AddSignalR(
                options =>
                {
                    if (this.Environment.IsDevelopment())
                    {
                        options.EnableDetailedErrors = true;
                    }
                });

            services.AddDefaultIdentity<VinylExchangeUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<VinylExchangeRole>().AddEntityFrameworkStores<VinylExchangeDbContext>();

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

            services.AddControllers(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
                .AddNewtonsoftJson();

            services.AddAntiforgery(
                options =>
                {
                    options.HeaderName = "RequestVerificationToken";
                    options.SuppressXFrameOptionsHeader = false;
                });

            services.AddDistributedMemoryCache();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });

            services.AddCors();

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
            services.AddTransient<ISalesEntityRetriever, SalesService>();
            services.AddTransient<IGenresEntityRetriever, GenresService>();

            // Tool Services
            services.AddTransient<IMemoryCache, MemoryCache>();
            services.AddTransient<IMemoryCacheFilesSevice, MemoryCacheFilesService>();
            services.AddTransient<IFileManager, FileManager>();

            services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddSingleton<IEmailSender>(new EmailSender(this.Configuration["SendGridKey"]));
        }
    }
}