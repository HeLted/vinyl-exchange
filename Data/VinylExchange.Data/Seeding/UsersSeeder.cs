namespace VinylExchange.Data.Seeding
{
    #region

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using VinylExchange.Data.Models;

    #endregion

    public class UsersSeeder
    {
        private readonly UserManager<VinylExchangeUser> userManager;

        public UsersSeeder(UserManager<VinylExchangeUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SeedAdmin()
        {
            if (await this.userManager.FindByNameAsync("sysadmin") == null)
            {
                var admin = new VinylExchangeUser { UserName = "sysadmin", Email = "sysadmin@vinylexchange.com" };

                admin.PasswordHash = this.userManager.PasswordHasher.HashPassword(admin, "aphextwindrukqs22");

                await this.userManager.CreateAsync(admin);
            }
        }
    }
}