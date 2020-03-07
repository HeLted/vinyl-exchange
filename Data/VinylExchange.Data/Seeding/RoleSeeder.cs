namespace VinylExchange.Data.Seeding
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    
    using VinylExchange.Data.Models;
    using static VinylExchange.Common.Constants.RolesConstants;

    public class RoleSeeder
    {
        private readonly RoleManager<VinylExchangeRole> roleManager;

        private readonly UserManager<VinylExchangeUser> userManager;

        public RoleSeeder(UserManager<VinylExchangeUser> userManager, RoleManager<VinylExchangeRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedRoles()
        {
            bool adminRoleExists = await this.roleManager.RoleExistsAsync(Admin);
            if (!adminRoleExists)
            {
                await this.roleManager.CreateAsync(new VinylExchangeRole(Admin));
            }

            bool userRoleExists = await this.roleManager.RoleExistsAsync(User);

            if (!userRoleExists)
            {
                await this.roleManager.CreateAsync(new VinylExchangeRole(User));
            }

            bool chuskaRoleExists = await this.roleManager.RoleExistsAsync(Chushka);

            if (!userRoleExists)
            {
                await this.roleManager.CreateAsync(new VinylExchangeRole(Chushka));
            }

            VinylExchangeUser user = await this.userManager.FindByNameAsync("sysadmin");

            if (!await this.userManager.IsInRoleAsync(user, Admin))
            {
                await this.userManager.AddToRoleAsync(user, Admin);
            }
        }
    }
}