namespace VinylExchange.Roles
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using VinylExchange.Data.Models;

    public class UserSeeder
    {
        private readonly UserManager<VinylExchangeUser> userManager;

        public UserSeeder(UserManager<VinylExchangeUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SeedAdmin()
        {
            if (await this.userManager.FindByNameAsync("sysadmin") == null)
            {
                VinylExchangeUser admin = new VinylExchangeUser()
                                              {
                                                  UserName = "sysadmin", Email = "sysadmin@vinylexchange.com",
                                              };

                admin.PasswordHash = this.userManager.PasswordHasher.HashPassword(admin, "aphextwindrukqs22");

                await this.userManager.CreateAsync(admin);
            }
        }
    }
}