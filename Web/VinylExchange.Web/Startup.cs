namespace VinylExchange.Web
{
    using System;
    using System.IO;
    using System.Reflection;

    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;

    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Data.Seeding;
    using VinylExchange.Services.Authentication;
    using VinylExchange.Services.Data.HelperServices.Sales.SaleLogs;
    using VinylExchange.Services.Data.HelperServices.Sales.SaleMessages;
    using VinylExchange.Services.Data.HelperServices.Users;
    using VinylExchange.Services.Data.MainServices.Addresses;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Services.EmaiSender;
    using VinylExchange.Services.Files;
    using VinylExchange.Services.HelperServices.Releases;
    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MainServices.Collections;
    using VinylExchange.Services.MainServices.Genres;
    using VinylExchange.Services.MainServices.Releases;
    using VinylExchange.Services.MainServices.Styles;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Services.MemoryCache;
    using VinylExchange.Web.Hubs.SaleChat;
    using VinylExchange.Web.Hubs.SaleLog;
    using VinylExchange.Web.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            using (IServiceScope serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                VinylExchangeDbContext dbContext =
                    serviceScope.ServiceProvider.GetRequiredService<VinylExchangeDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                serviceScope.ServiceProvider.GetRequiredService<UserManager<VinylExchangeUser>>();
                serviceScope.ServiceProvider.GetRequiredService<RoleManager<VinylExchangeRole>>();
                serviceScope.ServiceProvider.GetRequiredService<UserSeeder>().SeedAdmin().GetAwaiter().GetResult();
                serviceScope.ServiceProvider.GetRequiredService<RoleSeeder>().SeedRoles().GetAwaiter().GetResult();
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

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
                        string path = context.Request.Path.Value;

                        if (string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) || string.Equals(
                                path,
                                "/Authentication/Logout-Callback",
                                StringComparison.OrdinalIgnoreCase))
                        {
                            // The request token can be sent as a JavaScript-readable cookie, 
                            // and Angular uses it by default.
                            AntiforgeryTokenSet tokens = antiforgery.GetAndStoreTokens(context);
                            context.Response.Cookies.Append(
                                "XSRF-TOKEN",
                                tokens.RequestToken,
                                new CookieOptions() { HttpOnly = false });
                        }

                        return next(context);
                    });

            app.UseSpa(
                spa =>
                    {
                        spa.Options.SourcePath = "ClientApp";

                        if (env.IsDevelopment())
                        {
                            spa.UseReactDevelopmentServer(npmScript: "start");
                        }
                    });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {        

            services.AddDbContext<VinylExchangeDbContext>(
                options => { options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")); });

            services.AddSignalR(options =>
            {
                if (Environment.IsDevelopment())
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

            services.AddAuthentication().AddIdentityServerJwt();

            services.AddIdentityServer().AddApiAuthorization<VinylExchangeUser, VinylExchangeDbContext>();

            // .AddProfileService<ProfileService>();
            services.AddControllers(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                        // options.Filters.Add(typeof(AntiXssAttribute));
                    }).AddNewtonsoftJson();

            services.AddAntiforgery(
                options =>
                    {
                        options.HeaderName = "RequestVerificationToken";
                        options.SuppressXFrameOptionsHeader = false;
                    });

            // services.AddControllersWithViews();
            // services.AddRazorPages();
            services.AddDistributedMemoryCache();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });

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

            // Tool Services
            services.AddTransient<MemoryCacheManager>();
            services.AddTransient<IMemoryCacheFileSevice, MemoryCacheFileService>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<RoleSeeder>();
            services.AddTransient<UserSeeder>();
            services.AddSingleton<IEmailSender>(
                new EmailSender("SG.LtntWeJrTD - 310jTquhuBA.M5rZlCNcnuM7U7xYTT4w25vcaevdykIY1flFZV - Shec"));
        }
    }
}