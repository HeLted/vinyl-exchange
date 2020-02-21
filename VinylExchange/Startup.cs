using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models;
using VinylExchange.Services.Files;
using VinylExchange.Services.HelperServices;
using VinylExchange.Services.Logging;
using VinylExchange.Services.MainServices.Collections;
using VinylExchange.Services.MainServices.Genres;
using VinylExchange.Services.MainServices.Releases;
using VinylExchange.Services.MainServices.Styles;
using VinylExchange.Services.Mapping;
using VinylExchange.Services.MemoryCache;

namespace VinylExchange
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<VinylExchangeDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<VinylExchangeUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<VinylExchangeRole>()
                .AddEntityFrameworkStores<VinylExchangeDbContext>();

            services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            });


            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddIdentityServer()
                .AddApiAuthorization<VinylExchangeUser, VinylExchangeDbContext>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDistributedMemoryCache();

         

            services.AddSingleton(AutoMapperConfig.GetMapper(typeof(ModelGetAssemblyClass).GetTypeInfo().Assembly));

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            //Enitity Services

            services.AddTransient<IReleasesService, ReleasesService>();
            services.AddTransient<IGenresService, GenresService>();
            services.AddTransient<IStylesService, StylesService>();
            services.AddTransient<IReleaseFilesService, ReleaseFilesService>();
            services.AddTransient<ICollectionsService, CollectionsService>();

            //Tool Services
            services.AddTransient<MemoryCacheManager>();
            services.AddTransient<IMemoryCacheFileSevice, MemoryCacheFileService>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<ILoggerService, LoggerService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
         


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
            app.UseFileServer(new FileServerOptions
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
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute("Default",
                "api/{controller}/{id}",
                new { id = System.Web.Http.RouteParameter.Optional });
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");

                }
            });
        }
    }
}
