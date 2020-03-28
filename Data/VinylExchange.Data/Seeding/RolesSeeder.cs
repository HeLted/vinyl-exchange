namespace VinylExchange.Data.Seeding
{
    #region

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using VinylExchange.Common.Constants;
    using VinylExchange.Data.Models;

    #endregion

    public class RolesSeeder
    {
        private readonly RoleManager<VinylExchangeRole> roleManager;

        private readonly UserManager<VinylExchangeUser> userManager;

        public RolesSeeder(UserManager<VinylExchangeUser> userManager, RoleManager<VinylExchangeRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedRoles()
        {
            var adminRoleExists = await this.roleManager.RoleExistsAsync(Roles.Admin);
            if (!adminRoleExists)
            {
                await this.roleManager.CreateAsync(new VinylExchangeRole(Roles.Admin));
            }

            var userRoleExists = await this.roleManager.RoleExistsAsync(Roles.User);

            if (!userRoleExists)
            {
                await this.roleManager.CreateAsync(new VinylExchangeRole(Roles.User));
            }

            var user = await this.userManager.FindByNameAsync("sysadmin");

            if (!await this.userManager.IsInRoleAsync(user, Roles.Admin))
            {
                await this.userManager.AddToRoleAsync(user, Roles.Admin);
            }
        }
    }
}