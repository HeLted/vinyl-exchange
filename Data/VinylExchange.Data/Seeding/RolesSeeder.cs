namespace VinylExchange.Data.Seeding
{
    #region

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using VinylExchange.Common.Constants;
    using VinylExchange.Data.Models;
    using VinylExchange.Data.Seeding.Contracts;

    #endregion

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<VinylExchangeRole>>();

            await SeedRoleAsync(roleManager, Roles.Admin);
            await SeedRoleAsync(roleManager, Roles.User);
        }

        private static async Task SeedRoleAsync(RoleManager<VinylExchangeRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new VinylExchangeRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}