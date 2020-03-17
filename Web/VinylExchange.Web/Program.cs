namespace VinylExchange.Web
{
    #region

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    #endregion

    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}